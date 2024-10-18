using System.Diagnostics;

namespace YTDownload.App.Controlls
{
    public static class MediaPlayer
    {
        public static void Play(FileInfo file)
        {
            if (file.Extension == ".webm" || file.Extension == ".mp4")
            {
                try
                {
                    Process process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = file.FullName,
                            UseShellExecute = true,
                            CreateNoWindow = false
                        }
                    };

                    process.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao tentar reproduzir a mídia: {ex.Message}");
                }
            }
        }
    }
}
