FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ColorTesterAPI/ColorTesterAPI.csproj", "ColorTesterAPI/"]
RUN dotnet restore "ColorTesterAPI/ColorTesterAPI.csproj"
COPY . .
WORKDIR "/src/ColorTesterAPI"
RUN dotnet build "ColorTesterAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ColorTesterAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ColorTesterAPI.dll"]
