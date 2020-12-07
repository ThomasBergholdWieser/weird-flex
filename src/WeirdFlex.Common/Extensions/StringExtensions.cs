namespace System
{
    public static class StringExtensions
    {
        public static string? WithMaxLength(this string? value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value?.Substring(0, Math.Min(value.Length, maxLength));
        }

        public static string? WithWildcards(this string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Replace("*", "%");
        }

        public static string? FirstCharToUpper(this string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var arr = input.ToCharArray();
            arr[0] = char.ToUpper(arr[0]);
            return new string(arr);
        }
    }
}
