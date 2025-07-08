# ---------------------
# Base runtime image
# ---------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# ---------------------
# Build and publish for production
# ---------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# First copy just the project files needed for restore
COPY ["API/API.csproj", "API/"]
COPY ["Directory.Build.props", "./"]
COPY ["nuget.config", "./"]

# Restore dependencies
RUN dotnet restore "API/API.csproj" --verbosity normal

# Now copy everything else
COPY . .

# Build and publish
RUN dotnet publish "API/API.csproj" -c Release -o /app/publish

# ---------------------
# Final image for production
# ---------------------
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]

# ---------------------
# Dev image with proper tooling
# ---------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS dev
WORKDIR /app

# Copy project files first
COPY ["API/API.csproj", "API/"]
COPY ["Directory.Build.props", "./"]
COPY ["nuget.config", "./"]

# Restore including EF tools
RUN dotnet restore "API/API.csproj" --verbosity normal
RUN dotnet tool install --global dotnet-ef

# Copy everything else
COPY . .

EXPOSE 80
ENTRYPOINT ["dotnet", "watch", "run", "--project", "API/API.csproj"]
