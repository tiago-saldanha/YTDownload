using Microsoft.AspNetCore.StaticFiles;

namespace YTDownload.API.Helper
{
    public static class MimeType
    {
        public static string GetContentType(string filePath)
        {
            string contentType = string.Empty;
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(Path.GetFileName(filePath), out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
