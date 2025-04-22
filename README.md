
# 📺 YTDownload - Projeto ASP.NET Core

Projeto para download de vídeos e áudios do YouTube utilizando a biblioteca [YoutubeExplode](https://github.com/Tyrrrz/YoutubeExplode) e o [ffmpeg](https://www.ffmpeg.org).

---

## ✅ Requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Docker (opcional)

---

## 🚀 Opções de Execução

### 1. Aplicação ASP.NET Core Web

#### 🔧 Build e Execução

No diretório raiz do projeto, execute:

```bash
dotnet build && dotnet run
```

---

### 2. Aplicação Windows Forms

Também é possível executar como uma aplicação desktop:

- Abra a solução no Visual Studio
- Rode o projeto `WindowsApp`
- Execute o `Program.cs` para iniciar a interface gráfica

---

### 3. Aplicação Blazor WebAssembly

O YTDownload pode rodar como uma aplicação Blazor WebAssembly:

- Configure o projeto `YTDownload.Front` no ambiente WebAssembly no `Startup.cs`
- Siga as mesmas instruções de execução da aplicação ASP.NET Core

---

## 🐳 Execução com Docker

### 🔨 Build da imagem com `docker compose`

No diretório raiz, execute:

```bash
docker compose build
docker compose up -d
```

---

### 🌐 Build manual da imagem com Docker

```bash
docker build -f "YTDownload.Front\Dockerfile" -t ytdownload:dev .
```

---

### ▶️ Executar com `docker run`

```bash
docker run -dp 8080:80 \
  -e "ASPNETCORE_ENVIRONMENT=Development" \
  -e "ASPNETCORE_URLS=http://+:80" \
  --name ytdownload
  ytdownload:dev
```

---

## 📘 Documentação via Swagger

- Acesse: [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)

Caso a variável de ambiente `ASPNETCORE_ENVIRONMENT` esteja definida como `Development`, a interface [Swagger UI](https://swagger.io/tools/swagger-ui/) estará habilitada para facilitar a exploração da API.

Para ambientes de produção, defina `ASPNETCORE_ENVIRONMENT=Production` para desativar o Swagger.

---

## 📄 Licença

Este projeto está licenciado sob a Licença **LGPL**. Consulte o arquivo [LICENSE](License.txt) para mais detalhes.

---

## 👨‍💻 Desenvolvido por

Tiago (YTDownload)

---
