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

        private async void Download(object sender, EventArgs e)
        {
            var command = new DownloadVideoCommand { Url = textBoxUrlVideo.Text, Mp4 = false, Resolutiuon = "1080p" };
            var result = string.Empty;
            try
            {
                textBoxOutput.Text = string.Empty;
                result = await _service.DownloadVideo(command);
            }
            catch
            {
            }
            finally
            {
                textBoxOutput.Text = result;
                textBoxUrlVideo.Text = string.Empty;
            }
        }
    }
}
