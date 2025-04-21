using Microsoft.AspNetCore.StaticFiles;

namespace YTDownload.API.Helper
{
    public static class MimeType
    {
        public static string GetContentType(string filePath)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(Path.GetFileName(filePath), out string? contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
