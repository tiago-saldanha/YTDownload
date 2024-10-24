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
            Ok(false);
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
                var filePath = string.Empty;
                try
                {
                    ChangeEnableButtons();
                    var command = GetCommand(row);
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
                    Ok(true);
                    if (checkBoxConverterMp3.Checked) Converter(filePath);
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

        private async void Converter(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"O arquivo({filePath}) não existe.");
                return;
            }

            await _service.Converter(filePath);
        }

        private void ChangeEnableButtons()
        {
            buttonPlay.Enabled = !buttonPlay.Enabled;
            buttonDownloadInfo.Enabled = !buttonDownloadInfo.Enabled;
            buttonDownload.Enabled = !buttonDownload.Enabled;
        }

        private DownloadCommand GetCommand(DataGridViewRow row)
        {
            bool isAudioOnly = false;
            Boolean.TryParse(row.Cells["IsAudioOnly"].Value.ToString(), out isAudioOnly);
            var containerName = row.Cells["ContainerName"].Value.ToString();
            var audioCodec = row.Cells["AudioCodec"].Value?.ToString();
            var resolution = row.Cells["Resolution"].Value?.ToString();
            var videoCodec = row.Cells["VideoCodec"].Value?.ToString();
            var url = row.Cells["Url"].Value.ToString();
            var command = new DownloadCommand(url, containerName, videoCodec, resolution, audioCodec, isAudioOnly);
            return command;
        }

        private void Atualizar(object sender, EventArgs e)
        {
            textBoxUrlVideo.Clear();
            textBoxOutput.Clear();
            dataGridView.DataSource = null;
            Ok(false);
        }

        private void Ok(bool valid)
        {
            buttonOk.Image = valid ? Image.FromFile(Path.Combine(Environment.CurrentDirectory, "icons", "verificar-verde.png")) : Image.FromFile(Path.Combine(Environment.CurrentDirectory, "icons", "verificar-cinza.png"));
        }
    }
}
