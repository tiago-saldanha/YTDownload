## YTDownload - ASP.NET Core Project
 Project to download videos and audio from youtube using this [library](https://github.com/Tyrrrz/YoutubeExplode) and [ffmpeg](https://www.ffmpeg.org).

## Requirements
SDK net8.0

## Opções de Execução
### 1. ASP.NET Core Web Application

## Build and Run

To run this project enter the root directory and run `dotnet build` && `dotnet run`

## Run With `docker compose`

Enter the root directory and run

1 - Build the updated image aspnet-core `docker compose build`

2 - Run docker-compose.yml `docker compose up -d`

## How to build and start a image exposure on port 8080 for HTTP
```bash
docker build -f "C:\Users\Tiago\Source\Repos\YTDownload\YTDownload\Dockerfile" -t ytdownload:dev "C:\Users\Tiago\Source\Repos\YTDownload"
```

## Run with `docker run`
```bash
docker run -dp 8080:80 -p 8081:443 -e "ASPNETCORE_ENVIRONMENT=Development" -e "ASPNETCORE_URLS=http://+:80" ytdownload:dev
```

### 2. Windows Forms Application
Este projeto também pode ser executado como uma aplicação de desktop Windows Forms. Basta rodar o Projeto WindowsApp o Program.cs para iniciar a interface gráfica.

### 3. Blazor WebAssembly Application
O YTDownload também pode ser executado como uma aplicação Blazor. Para isso, configure o projeto para o ambiente de WebAssembly(YTDownload.Front) no Startup.cs e siga as instruções de execução como para ASP.NET Core.

## Download via Swagger
http://localhost:8080/swagger/index.html

If you set the environment variable ASPNETCORE_ENVIRONMENT to Develpment, [Swagger UI](https://swagger.io/tools/swagger-ui/) will be enabled, this will help you read the API documentation.

You can also set the environment variable ASPNETCORE_ENVIRONMENT for Production to disable [Swagger UI](https://swagger.io/tools/swagger-ui/).

Try in browser: http://localhost:8080/swagger/index.html

## Licença

Este projeto está licenciado sob a Licença LGPL - veja o arquivo [LICENSE](License.txt) para detalhes.
