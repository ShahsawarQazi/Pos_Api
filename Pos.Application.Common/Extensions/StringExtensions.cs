using System.Text.RegularExpressions;

namespace Pos.Application.Common.Extensions
{
    public static class StringExtensions
    {
        internal static string SplitWords(this string str)
        {
            var words = Regex.Split(str, "(?<=\\p{Ll})(?=\\p{Lu})|(?<=\\p{L})(?=\\p{Lu}\\p{Ll})");

            var header = string.Join(" ", words);

            return header;
        }
        public static TEnum ToEnum<TEnum>(this string value, TEnum defaultValue) where TEnum : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            TEnum result;
            return Enum.TryParse<TEnum>(value, true, out result) ? result : defaultValue;
        }
        internal static string RemoveEmptyLines(this string str)
        {
            return Regex.Replace(str, @"^(\s|\t)+$[\r\n]*", string.Empty, RegexOptions.Multiline);
        }

        internal static string CheckIfNullThenDefault(this string str, string name)
        {
            return string.IsNullOrEmpty(str) ? string.Empty : $"{name.SplitWords()}: {str}";
        }

        internal static string CheckIfNullThenDefault(this uint str, string name)
        {
            return str == 0 ? string.Empty : $"{name.SplitWords()}: {str}";
        }

        internal static string CheckIfNullThenDefault(this ulong str, string name)
        {
            return str == 0 ? string.Empty : $"{name.SplitWords()}: {str}";
        }

        internal static string AppendTabAtStartOfLine(this string s, int numOfTabs = 1)
        {
            var tabs = "\t";
            for (var i = 1; i < numOfTabs; i++)
                tabs += "\t";

            var r = new Regex(@"^", RegexOptions.Multiline);
            return r.Replace(s, tabs);
        }



    }
}
