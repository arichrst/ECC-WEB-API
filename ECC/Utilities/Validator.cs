namespace ECC
{
    public static class Validator
    {
        public static bool IsEmpty(this string text)
        {
            return string.IsNullOrEmpty(text) && string.IsNullOrWhiteSpace(text);
        }
    }
}