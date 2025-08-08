# Usar imagen oficial de .NET 9
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar archivos de proyecto
COPY ["Web/Web.csproj", "Web/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Entity/Entity.csproj", "Entity/"]
COPY ["Utilities/Utilities.csproj", "Utilities/"]

# Restaurar dependencias
RUN dotnet restore "Web/Web.csproj"

# Copiar el resto del código
COPY . .

# Build del proyecto
WORKDIR "/src/Web"
RUN dotnet build "Web.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app/publish

# Runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Variables de entorno
ENV ASPNETCORE_URLS=http://*:80
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Web.dll"]