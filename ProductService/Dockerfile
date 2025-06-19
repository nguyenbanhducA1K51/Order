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
COPY . .
RUN dotnet restore "./API/API.csproj"
RUN dotnet publish "./API/API.csproj" -c Release -o /app/publish

# ---------------------
# Final image for production
# ---------------------
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]

# ---------------------
# Dev image with dotnet watch (bind-mount source code)
# ---------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS dev
WORKDIR /app
COPY . .
EXPOSE 80
ENTRYPOINT ["dotnet", "watch", "run", "--project", "API/API.csproj"]