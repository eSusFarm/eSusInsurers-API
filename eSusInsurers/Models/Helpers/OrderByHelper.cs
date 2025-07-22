namespace eSusInsurers.Models.Helpers
{
    public static class OrderByHelper
    {
        public static string? GetOrderByParams(Dictionary<string, Common.Filter> filters)
        {
            const string key = "OrderBy";
            if (filters.ContainsKey(key))
            {
                return ConvertToPascalCase(filters[key].Value);
            }
            return null;
        }

        public static string ConvertToPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var result = string.Empty;
            var capitalizeNext = true;

            for (var i = 0; i < input.Length; i++)
            {
                var character = input[i];
                if (character == '_')
                {
                    capitalizeNext = true;
                }
                else
                {
                    result += capitalizeNext ? char.ToUpper(character) : char.ToLower(character);
                    capitalizeNext = false;
                }
            }
            return result;
        }
    }
}
