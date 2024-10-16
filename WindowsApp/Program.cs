using YTDownload.App.DI;

namespace WindowsApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(ContainerDI.ConfigureApp());
        }
    }
}