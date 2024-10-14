## YTDownload - ASP.NET Core Project
 Project to download videos and audio from youtube using this [library](https://github.com/Tyrrrz/YoutubeExplode) and [ffmpeg](https://www.ffmpeg.org).

## Requirements
SDK net8.0

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

## Download via Swagger
http://localhost:8080/swagger/index.html

### Download Mp3 via WSL
Run with `vim .bashrc` and paste

```bash
function download_youtube_audio {
        local url=$1
        local output_file=$2

        curl -X POST 'http://localhost:8080/Video/DownloadAudioMp3' \
                -H 'accept: application/json' \
                -H 'Content-Type: application/json' \
                -d "{ \"url\": \"$url\" }" \
                --output "$output_file"
}
```
Run `source ~/.bashrc` to reload ~/.bashrc

Ex on wsl.: download_youtube_audio "https://www.youtube.com/watch?v=dlGOiuxSzVw" "teste.mp3"

If you set the environment variable ASPNETCORE_ENVIRONMENT to Develpment, [Swagger UI](https://swagger.io/tools/swagger-ui/) will be enabled, this will help you read the API documentation.

You can also set the environment variable ASPNETCORE_ENVIRONMENT for Production to disable [Swagger UI](https://swagger.io/tools/swagger-ui/).

Try in browser: http://localhost:8080/swagger/index.html

## Licença

Este projeto está licenciado sob a Licença LGPL - veja o arquivo [LICENSE](License.txt) para detalhes.
