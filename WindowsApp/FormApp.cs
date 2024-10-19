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
            if (string.IsNullOrEmpty(textBoxUrlVideo.Text))
            {
                MessageBox.Show($"Informe uma url válida!");
                return;
            }

            var filePath = string.Empty;
            try
            {
                ChangeEnableButtons();
                var command = new DownloadVideoCommand { Url = textBoxUrlVideo.Text, Mp4 = checkBoxConverterMp3Mp4.Checked, Resolutiuon = "1080p" };
                filePath = await _service.DownloadVideo(command);
                textBoxOutput.Clear();
            }
            catch
            {
            }
            finally
            {
                textBoxOutput.Text = filePath;
                textBoxUrlVideo.Clear();
                if (checkBoxAutoPlay.Checked && !string.IsNullOrEmpty(filePath)) Play(filePath);
                ChangeEnableButtons();
            }
        }

        private async void DownloadAudio(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxUrlVideo.Text))
            {
                MessageBox.Show($"Informe uma url válida!");
                return;
            }

            var filePath = string.Empty;
            try
            {
                ChangeEnableButtons();
                DownloadAudioCommand command = new DownloadAudioCommand { Url = textBoxUrlVideo.Text, Mp3 = checkBoxConverterMp3Mp4.Checked };
                filePath = await _service.DownloadAudio(command);
                textBoxOutput.Clear();
            }
            catch
            {
            }
            finally 
            { 
                textBoxOutput.Text = filePath;
                textBoxUrlVideo.Clear();
                if (checkBoxAutoPlay.Checked) Play(filePath);
                ChangeEnableButtons();
            }
        }

        private void Play(object sender, EventArgs e)
        {
            ChangeEnableButtons();

            if (string.IsNullOrEmpty(textBoxOutput.Text))
                MessageBox.Show($"Nenhum arquivo lozalido!");
            else
                Play(textBoxOutput.Text);

            ChangeEnableButtons();
        }

        private void Play(string filePath) => MediaPlayer.Play(filePath);

        private void ChangeEnableButtons()
        {
            buttonDownloadVideo.Enabled = !buttonDownloadVideo.Enabled;
            buttonDownloadAudio.Enabled = !buttonDownloadAudio.Enabled;
            buttonPlay.Enabled = !buttonPlay.Enabled;
        }
    }
}
