using YTDownload.App.Controlls;
using YTDownload.Application.Commands;
using YTDownload.Application.Interfaces;

namespace WindowsApp
{
    public partial class FormApp : Form
    {
        private readonly IYoutubeService _service;

        public FormApp(IYoutubeService service)
        {
            InitializeComponent();
            _service = service;
        }

        private async void DownloadVideo(object sender, EventArgs e)
        {
            var command = new DownloadVideoCommand { Url = textBoxUrlVideo.Text, Mp4 = false, Resolutiuon = "1080p" };
            var filePath = string.Empty;
            try
            {
                textBoxOutput.Clear();
                filePath = await _service.DownloadVideo(command);
            }
            catch
            {
            }
            finally
            {
                textBoxOutput.Text = filePath;
                textBoxUrlVideo.Clear();
            }
        }

        private void Play(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxOutput.Text))
            {
                FileInfo fileInfo = new FileInfo(textBoxOutput.Text);
                MediaPlayer.Play(fileInfo);
            }
        }

        private async void DownloadAudio(object sender, EventArgs e)
        {
            DownloadAudioCommand command = new DownloadAudioCommand { Url = textBoxUrlVideo.Text, Mp3 = true };
            var filePath = string.Empty;
            try
            {
                textBoxOutput.Clear();
                filePath = await _service.DownloadAudio(command);
            }
            catch
            {
            }
            finally 
            { 
                textBoxOutput.Text = filePath;
                textBoxUrlVideo.Clear();
            }
        }
    }
}
