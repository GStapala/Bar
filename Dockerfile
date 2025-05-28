FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Install Node.js and npm
RUN apt-get update && apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g npm@latest

USER $APP_UID

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Install Node.js and npm in the build stage
RUN apt-get update && apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g npm@latest

COPY ["BarBackend/src/Web/Web.csproj", "BarBackend/src/Web/"]
COPY ["BarBackend/src/Application/Application.csproj", "BarBackend/src/Application/"]
COPY ["BarBackend/src/Domain/Domain.csproj", "BarBackend/src/Domain/"]
COPY ["BarBackend/src/Infrastructure/Infrastructure.csproj", "BarBackend/src/Infrastructure/"]
COPY ["BarBackend/Directory.Build.props", "./"]
COPY ["BarBackend/Directory.Packages.props", "./"]
RUN dotnet restore "BarBackend/src/Web/Web.csproj"
COPY . .
WORKDIR "/src/BarBackend/src/Web"
RUN dotnet build "./Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BarBackend.Web.dll"]