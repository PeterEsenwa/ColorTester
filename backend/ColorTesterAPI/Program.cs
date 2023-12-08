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

var builder = WebApplication.CreateBuilder(args);

// builder.Configuration.Sources.Clear();
// DotEnv.Load();
// builder.Configuration.AddEnvironmentVariables();

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

// Add services to the container.
builder.Services.AddPostgresSQLDependencyInjection(builder.Configuration);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new PaletteColorValueConverter());
        options.SerializerSettings.Converters.Add(new ColorConverter());
        options.SerializerSettings.Converters.Add(new ColorDtoConverter());
        options.SerializerSettings.Converters.Add(new ColorIntensityConverter());
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
        options.Audience = builder.Configuration["Auth0:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });

if (builder.Environment.IsDevelopment())
{
    /*builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name = "auth";
            options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
            options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.None;
            options.Cookie.HttpOnly = false;
            options.Cookie.IsEssential = true;
            options.LoginPath = "/login";
            options.LogoutPath = "/logout";
        });*/
    
    builder.Services
        .AddAuthorization(options =>
        {
            var roles = new Dictionary<string, string>
            {
                {"PaletteCreator", "create:palettes"},
                {"PaletteReader", "read:palettes"},
                {"PaletteUpdater", "update:palettes"},
                {"PaletteDeleter", "delete:palettes"}
            };
            
            foreach (var (role, scope) in roles)
            {
                options.AddPolicy(
                    role,
                    policy => policy.Requirements.Add(
                        new HasScopeRequirement(scope, $"https://{builder.Configuration["Auth0:Domain"]}/")
                    )
                );
            }
        });
}

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ColorTesterAPI v1");
        
        c.OAuthClientId(builder.Configuration["Auth0:ClientId"]);
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();