using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkSFrancis.DotNetExtensions
{
    public static class StringExtensions
    {
        public static string Join<T>(this IEnumerable<T> str, string seperator)
        {
            return string.Join(seperator, str);
        }

        public static string CsvSanitise(this string str)
        {
            var retVal = str.Replace("\"", "\"\"");

            if (str.Contains('\"') || str.Contains(',') || str.Contains('\n') || str.Contains('\r'))
            {
                retVal = '\"' + retVal + '\"';
            }

            return retVal;
        }

        public static bool IsEmptyOrWhitespace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsEmpty(this string str)
        {
            return string.Empty == str;
        }

        public static string Remove(this string s, params char[] charsToRemove)
        {
            return Remove(s, (IEnumerable<char>)charsToRemove);
        }

        public static string Remove(this string s, IEnumerable<char> charsToRemove)
        {
            string retString = s;
            foreach (var c in charsToRemove)
            {
                retString = retString.Replace(c.ToString(), string.Empty);
            }

            return retString;
        }

        public static string Remove(this string s, params string[] textToRemove)
        {
            return Remove(s, (IEnumerable<string>)textToRemove);
        }

        public static string Remove(this string s, IEnumerable<string> textToRemove)
        {
            string retString = s;
            foreach (var text in textToRemove)
            {
                retString = retString.Replace(text, string.Empty);
            }

            return retString;
        }

        public static string ToCamelCase(this string str, bool firstCharIsUpper, params string[] wordDelimiters)
        {
            if (wordDelimiters.Length == 0 || wordDelimiters == null)
            {
                wordDelimiters = new[] { " ", "-" };
            }

            var words = str.Split(wordDelimiters, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
            {
                return string.Empty;
            }

            string MakeWordCamelCase(string word)
            {
                if (word.Length == 0)
                {
                    return string.Empty;
                }

                return char.ToUpper(word[0]) + word.Substring(1, word.Length - 1).ToLower();
            }

            StringBuilder camelCase = new StringBuilder(str.Length);

            camelCase.Append(firstCharIsUpper ? MakeWordCamelCase(words[0]) : words[0].ToLower());

            for(int wordIndex = 1; wordIndex < words.Length; ++wordIndex)
            {
                camelCase.Append(MakeWordCamelCase(words[wordIndex]));
            }

            return camelCase.ToString();
        }
    }
}