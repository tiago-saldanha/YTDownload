FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["YTDownload/YTDownload.csproj", "YTDownload/"]
COPY ["Application/Application.csproj", "Application/"]
RUN dotnet restore "./YTDownload/YTDownload.csproj"
COPY . .
WORKDIR "/src/YTDownload"
RUN dotnet build "./YTDownload.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./YTDownload.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY YTDownload/lib /app/lib
ENTRYPOINT ["dotnet", "YTDownload.dll"]