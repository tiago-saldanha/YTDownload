using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using YTDownload.Application.Commands;
using YTDownload.Application.ViewModel;
using YTDownload.Front.Models;

namespace YTDownload.Front.Components.Pages.Youtube
{
    public partial class Index
    {
        private string Url { get; set; }
        private List<StreamViewModel> Streams = new();
        private bool Loading { get; set; } = true;
        private string Message { get; set; } = string.Empty;
        [Inject] IJSRuntime JSRuntime { get; set; }

        private async Task GetManifestInfo()
        {
            if (string.IsNullOrEmpty(Url))
            {
                Message = "Por favor, insira uma URL válida.";
                return;
            }

            Message = "Pesquisando...";
            Loading = true;

            try
            {
                List<StreamManifestViewModel> streams = await _service.DownloadManifestInfo(Url);
                if (streams.Any() && streams.Count > 0)
                {
                    Streams.Clear();
                    Streams.AddRange(streams.Select(s => new StreamViewModel(s.ContainerName, s.VideoCodec, s.Resolution, s.Size, s.IsAudioOnly, s.AudioCodec, s.Url)));
                    Loading = false;
                }
                else
                {
                    Message = "Nenhum stream encontrado.";
                }
            }
            catch (Exception ex)
            {
                Message = $"Erro: {ex.InnerException.Message}";
            }
        }

        private async Task SubmitSelection(StreamViewModel stream)
        {
            Message = "Baixando...";
            Loading = true;
            var command = new DownloadCommand(stream.Url, stream.ContainerName, stream.VideoCodec, stream.Resolution, stream.AudioCodec, stream.IsAudioOnly);
            var filePath = await _service.Download(command);

            if (!File.Exists(filePath))
            {
                Message = filePath.Contains("ffmpeg") ? "Ocorreu um erro ao realizar o download do formato selecionado, por favor tente outro formato." : filePath.ToString();
            }
            else
            {
                var fileUrl = $"/downloads/{Uri.EscapeDataString(Path.GetFileName(filePath))}";
                await JSRuntime.InvokeVoidAsync("downloadFileFromPath", fileUrl);
                Loading = false;
            }
        }
    }
}
