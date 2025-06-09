# ===============================
# API Gateway
# ===============================

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ../../../src/Challenge.SolutionArchitecture.ApiGateway/*.csproj ./Challenge.SolutionArchitecture.ApiGateway/
RUN dotnet restore ./Challenge.SolutionArchitecture.ApiGateway/Challenge.SolutionArchitecture.ApiGateway.csproj

COPY ../../../src/Challenge.SolutionArchitecture.ApiGateway/ ./Challenge.SolutionArchitecture.ApiGateway/
COPY docker/api-gateway/appsettings.json ./Challenge.SolutionArchitecture.ApiGateway/appsettings.json

WORKDIR /src/Challenge.SolutionArchitecture.ApiGateway
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Challenge.SolutionArchitecture.ApiGateway.dll"]
