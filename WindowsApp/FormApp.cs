using YoutubeExplode.Common;
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

        private async void DownloadManifestInfo(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxUrlVideo.Text))
            {
                MessageBox.Show($"Informe uma url válida!");
                return;
            }

            try
            {
                ChangeEnableButtons();
                var streams = await _service.DownloadManifestInfo(textBoxUrlVideo.Text);
                if (streams != null && streams.Any())
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = streams.Select(stream => new
                    {
                        ContainerName = stream.Format,
                        MegaBytes = Math.Round(stream.Size, 2),
                        Resolution = stream.Resolution ?? null,
                        VideoCodec = stream.VideoCodec ?? null,
                        IsAudioOnly = stream.AudioCodec != null,
                        AudioCodec = stream.AudioCodec ?? null,
                        Url = textBoxUrlVideo.Text
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao baixar informações do manifest: {ex.Message}");
            }
            finally
            {
                ChangeEnableButtons();
                textBoxUrlVideo.Clear();
            }
        }

        private async void Download(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var row = dataGridView.SelectedRows[0];
                bool isAudioOnly = false;
                Boolean.TryParse(row.Cells["IsAudioOnly"].Value.ToString(), out isAudioOnly);
                var containerName = row.Cells["ContainerName"].Value.ToString();
                var audioCodec = row.Cells["AudioCodec"].Value?.ToString();
                var resolution = row.Cells["Resolution"].Value?.ToString();
                var videoCodec = row.Cells["VideoCodec"].Value?.ToString();
                var url = row.Cells["Url"].Value.ToString();

                var filePath = string.Empty;
                try
                {
                    ChangeEnableButtons();
                    var command = new DownloadCommand(url, containerName, videoCodec, resolution, audioCodec, isAudioOnly);
                    filePath = await _service.Download(command);
                    textBoxOutput.Clear();
                }
                catch
                {
                }
                finally
                {
                    textBoxOutput.Text = filePath;

                    if (!File.Exists(filePath)) MessageBox.Show($"Erro ao baixar o arquivo: {filePath}");
                    textBoxUrlVideo.Clear();
                    if (checkBoxAutoPlay.Checked && !string.IsNullOrEmpty(filePath)) Play(filePath);
                    ChangeEnableButtons();
                }
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
            buttonPlay.Enabled = !buttonPlay.Enabled;
            buttonDownloadInfo.Enabled = !buttonDownloadInfo.Enabled;
            buttonDownload.Enabled = !buttonDownload.Enabled;
        }

    }
}
