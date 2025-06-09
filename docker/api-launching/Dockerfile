# ===============================
# Launching Service
# ===============================

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ../../../src/Challenge.SolutionArchitecture.LaunchingService/*.csproj ./Challenge.SolutionArchitecture.LaunchingService/
RUN dotnet restore ./Challenge.SolutionArchitecture.LaunchingService/Challenge.SolutionArchitecture.LaunchingService.csproj

COPY ../../../src/Challenge.SolutionArchitecture.LaunchingService/ ./Challenge.SolutionArchitecture.LaunchingService/
COPY docker/api-launching/appsettings.json ./Challenge.SolutionArchitecture.LaunchingService/appsettings.json

WORKDIR /src/Challenge.SolutionArchitecture.LaunchingService
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Challenge.SolutionArchitecture.LaunchingService.dll"]
