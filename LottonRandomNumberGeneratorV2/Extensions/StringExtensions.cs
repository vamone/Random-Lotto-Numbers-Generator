using System.Text.RegularExpressions;

namespace LottonRandomNumberGeneratorV2.Extensions;

public static class StringExtensions
{
    public static bool IsDigitsOnly(this string value)
    {
        foreach (char c in value)
        {
            if (c < '0' || c > '9')
                return false;
        }

        return true;
    }

    public static int ToInt32(this string value)
    {
        int.TryParse(value, out int returnValue);

        return returnValue;
    }

    public static bool IsRegexMatches(this string value, string regexPattern)
    {
        var matches = Regex.Match(value, regexPattern);
        return matches.Success;
    }
}