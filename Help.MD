## Docker Image
docker build -f "C:\Users\Tiago\Source\Repos\YTDownload\YTDownload\Dockerfile" -t ytdownload:dev "C:\Users\Tiago\Source\Repos\YTDownload"

## Docker Run
docker run -dp 8080:80 -p 8081:443 -e "ASPNETCORE_ENVIRONMENT=Development" -e "ASPNETCORE_URLS=http://+:80" ytdownload:dev

## Download via Swagger
http://localhost:8080/swagger/index.html

## Download Mp3 via WSL
vim .bashrc and paste

'''
function download_youtube_audio {
        local url=$1
        local output_file=$2

        curl -X POST 'http://localhost:8080/Video/DownloadAudioMp3' \
                -H 'accept: application/json' \
                -H 'Content-Type: application/json' \
                -d "{ \"url\": \"$url\" }" \
                --output "$output_file"
}
'''

source ~/.bashrc
Ex.: download_youtube_audio "https://www.youtube.com/watch?v=dlGOiuxSzVw" "/mnt/c/Users/Tiago/Music/teste.mp3"