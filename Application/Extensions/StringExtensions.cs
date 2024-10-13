namespace Application.Core.Extensions
{
    public static class StringExtensions
    {
        public static string FormaterName(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return input
                .Replace("/", "")
                .Replace("|", "")
                .Replace("^", "")
                .Replace("!", "")
                .Replace("@", "")
                .Replace("#", "")
                .Replace("$", "")
                .Replace("%", "")
                .Replace("&", "")
                .Replace("*", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("+", "")
                .Replace("=", "")
                .Replace("{", "")
                .Replace("}", "")
                .Replace("[", "")
                .Replace("]", "")
                .Replace(";", "")
                .Replace(":", "")
                .Replace("/", "");
        }
    }
}
