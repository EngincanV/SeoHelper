namespace SeoHelper.Extensions
{
    internal static class StringExtensions
    {
        internal static string EnsureStartsWith(this string text, char letter)
        {
            if (text.StartsWith(letter))
            {
                return text;
            }

            return letter + text;
        }

        internal static string EnsureEndsWith(this string text, char letter)
        {
            if (text.EndsWith(letter))
            {
                return text;
            }

            return text + letter;
        }
    }
}