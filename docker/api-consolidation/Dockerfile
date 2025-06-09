# ===============================
# Consolidation Service
# ===============================

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ../../../src/Challenge.SolutionArchitecture.ConsolidationService/*.csproj ./Challenge.SolutionArchitecture.ConsolidationService/
RUN dotnet restore ./Challenge.SolutionArchitecture.ConsolidationService/Challenge.SolutionArchitecture.ConsolidationService.csproj

COPY ../../../src/Challenge.SolutionArchitecture.ConsolidationService/ ./Challenge.SolutionArchitecture.ConsolidationService/
COPY docker/api-consolidation/appsettings.json ./Challenge.SolutionArchitecture.ConsolidationService/appsettings.json

WORKDIR /src/Challenge.SolutionArchitecture.ConsolidationService
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Challenge.SolutionArchitecture.ConsolidationService.dll"]
