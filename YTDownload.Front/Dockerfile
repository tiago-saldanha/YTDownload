# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# Corrige permissões da pasta como root (antes de mudar usuário!)
RUN mkdir -p /app/wwwroot/downloads && chmod -R 777 /app/wwwroot
USER app
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["YTDownload.Front/YTDownload.Front.csproj", "YTDownload.Front/"]
COPY ["Application/YTDownload.Application.csproj", "Application/"]
COPY ["YTDownload.CrossCutting/YTDownload.CrossCutting.csproj", "YTDownload.CrossCutting/"]
RUN dotnet restore "./YTDownload.Front/YTDownload.Front.csproj"
COPY . .
WORKDIR "/src/YTDownload.Front"
RUN dotnet build "./YTDownload.Front.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./YTDownload.Front.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YTDownload.Front.dll"]