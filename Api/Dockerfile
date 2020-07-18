FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY *.sln .
COPY Api/*.csproj ./Api/
COPY Domain/*.csproj ./Domain/
COPY Data/*.csproj ./Data/
COPY UnitTests/*.csproj ./UnitTests/
COPY IntegrationTests/*.csproj ./IntegrationTests/

RUN dotnet restore

COPY Api/. ./Api/
COPY Domain/. ./Domain/
COPY Data/. ./Data/
COPY UnitTests/. ./UnitTests/
COPY IntegrationTests/. ./IntegrationTests/

RUN dotnet test -c Release

WORKDIR /app/Api
RUN dotnet publish -c Release -o out

HEALTHCHECK --interval=15m --timeout=10s --retries=3 --start-period=1m CMD curl --fail http://localhost:80/health || exit 1

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
EXPOSE 80
WORKDIR /app
COPY --from=build /app/Api/out .
ENTRYPOINT ["dotnet", "Api.dll"]