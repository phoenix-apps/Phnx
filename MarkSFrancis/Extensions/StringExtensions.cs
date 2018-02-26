using System;
using System.Collections.Generic;
using System.Text;

namespace MarkSFrancis.Extensions
{
    /// <summary>
    /// Extensions for <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {

    /// <summary>Concatenates the members of a collection, using the specified separator between each member</summary>
    /// <param name="separator">The string to use as a separator. The separator is only included in the returned string if <paramref name="values"/> has more than one element</param>
    /// <param name="values">A collection that contains the objects to concatenate</param>
    /// <typeparam name="T">The type of the members of <paramref name="values"/></typeparam>
    /// <returns>A string that consists of the members of <paramref name="values"/> delimited by the <paramref name="separator">separator</paramref> string. If <paramref name="values"/> has no members, the method returns <see cref="String.Empty"></see></returns>
    /// <exception cref="System.ArgumentNullException"><paramref name="values"/> is null.</exception>
        public static string Join<T>(this IEnumerable<T> values, string separator)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// Get if this string is <see langword="null"/>, <see cref="string.Empty"/>, or whitespace
        /// </summary>
        /// <param name="str">The string to check</param>
        /// <returns></returns>
        public static bool IsEmptyOrWhitespace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Get if this string is <see langword="null"/>, <see cref="string.Empty"/>
        /// </summary>
        /// <param name="str">The string to check</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Removes a series of characters from a string
        /// </summary>
        /// <param name="s">The string to remove the characters from</param>
        /// <param name="charsToRemove">The characters to remove</param>
        /// <returns></returns>
        public static string Remove(this string s, params char[] charsToRemove)
        {
            return Remove(s, (IEnumerable<char>)charsToRemove);
        }
        
        /// <summary>
        /// Removes a series of characters from a string
        /// </summary>
        /// <param name="s">The string to remove the characters from</param>
        /// <param name="charsToRemove">The characters to remove</param>
        /// <returns></returns>
        public static string Remove(this string s, IEnumerable<char> charsToRemove)
        {
            string retString = s;
            foreach (var c in charsToRemove)
            {
                retString = retString.Replace(c.ToString(), string.Empty);
            }

            return retString;
        }
        
        /// <summary>
        /// Removes a series of strings from a string
        /// </summary>
        /// <param name="s">The string to remove the strings from</param>
        /// <param name="textToRemove">The strings to remove</param>
        /// <returns></returns>
        public static string Remove(this string s, params string[] textToRemove)
        {
            return Remove(s, (IEnumerable<string>)textToRemove);
        }
        
        /// <summary>
        /// Removes a series of strings from a string
        /// </summary>
        /// <param name="s">The string to remove the strings from</param>
        /// <param name="textToRemove">The strings to remove</param>
        /// <returns></returns>
        public static string Remove(this string s, IEnumerable<string> textToRemove)
        {
            string retString = s;
            foreach (var text in textToRemove)
            {
                retString = retString.Replace(text, string.Empty);
            }

            return retString;
        }

        /// <summary>
        /// Converts a string to camel case, using <paramref name="wordDelimiters"/> to indicate where each word begins
        /// </summary>
        /// <param name="str">The string to convert</param>
        /// <param name="firstCharIsUpper">Whether the first character should be upper case. Setting this to true is the same as <see cref="ToPascalCase"/></param>
        /// <param name="wordDelimiters">The strings that seperate each word. If none are given, this is set to "-", "_" and " "</param>
        /// <returns></returns>
        public static string ToCamelCase(this string str, bool firstCharIsUpper, params string[] wordDelimiters)
        {
            if ( wordDelimiters == null || wordDelimiters.Length == 0)
            {
                wordDelimiters = new[] { " ", "-", "_" };
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

        /// <summary>
        /// Converts a string to camel case, using <paramref name="wordDelimiters"/> to indicate where each word begins
        /// </summary>
        /// <param name="str">The string to convert</param>
        /// <param name="wordDelimiters">The strings that seperate each word. If none are given, this is set to "-", "_" and " "</param>
        /// <returns></returns>
        public static string ToPascalCase(this string str, params string[] wordDelimiters)
        {
            return ToCamelCase(str, true, wordDelimiters);
        }
    }
}