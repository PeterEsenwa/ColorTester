using System.Security.Claims;
// using dotenv.net;
using ColorTesterAPI;
using ColorTesterAPI.Application.Converters;
using ColorTesterAPI.Infrastructure.Persistence.SQL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

public class Program
{
    public static readonly Dictionary<string, string> RolesDictionary = new()
    {
        { "PaletteCreator", "create:palettes" },
        { "PaletteReader", "read:palettes" },
        { "PaletteUpdater", "update:palettes" },
        { "PaletteDeleter", "delete:palettes" }
    };
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder);
        
        var app = builder.Build();
        Configure(app);
        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSingleton<IAuthorizationHandler, HasPermissionHandler>();

        ConfigureCors(builder);
        ConfigureDatabase(builder);
        ConfigureControllers(builder);
        ConfigureSwagger(builder);
        ConfigureAuthentication(builder);
        ConfigureAuthorization(builder);
    }

    private static void Configure(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ColorTesterAPI v1");
            });
        }
        
        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }

    private static void ConfigureCors(WebApplicationBuilder builder)
    {
        // Add CORS policy
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", b =>
                {
                    b.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }
    }

    private static void ConfigureDatabase(WebApplicationBuilder builder)
    {
        // Add database context
        builder.Services.AddPostgresSQLDependencyInjection(builder.Configuration);
    }

    private static void ConfigureControllers(WebApplicationBuilder builder)
    {
        // Add controllers
        builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new PaletteColorValueConverter());
                options.SerializerSettings.Converters.Add(new ColorConverter());
                options.SerializerSettings.Converters.Add(new ColorDtoConverter());
                options.SerializerSettings.Converters.Add(new ColorIntensityConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
    }

    private static void ConfigureSwagger(WebApplicationBuilder builder)
    {
        // Add Swagger
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "ColorTesterAPI", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.<br/> 
                        Enter 'Bearer' [space] and then your token in the text input below.<br/>
                        Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
    
    private static void ConfigureAuthentication(WebApplicationBuilder builder)
    {
        // Add authentication
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["Auth0:Domain"];
                options.Audience = builder.Configuration["Auth0:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });
    }
    
    private static void ConfigureAuthorization(WebApplicationBuilder builder)
    {
        // Add authorization
        builder.Services.AddAuthorization(options =>
        {
            var roles = RolesDictionary;

            foreach (var (role, scope) in roles)
            {
                options.AddPolicy(
                    role,
                    policy => policy.Requirements.Add(
                        new HasPermissionRequirement(scope, builder.Configuration["Auth0:Domain"] ?? throw new InvalidOperationException())
                    )
                );
            }
        });
    }
}
