using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is null.</exception>
        public static string Join<T>(this IEnumerable<T> values, string separator)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// Get if this string is <see langword="null"/>, <see cref="string.Empty"/>, or whitespace
        /// </summary>
        /// <param name="stringToTest">The string to check</param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrWhitespace(this string stringToTest)
        {
            return string.IsNullOrWhiteSpace(stringToTest);
        }

        /// <summary>
        /// Get if this string is <see langword="null"/> or <see cref="string.Empty"/>
        /// </summary>
        /// <param name="stringToTest">The string to check</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string stringToTest)
        {
            return string.IsNullOrEmpty(stringToTest);
        }

        /// <summary>
        /// Removes a series of characters from a string
        /// </summary>
        /// <param name="stringToRemoveFrom">The string to remove the characters from</param>
        /// <param name="charsToRemove">The characters to remove</param>
        /// <returns></returns>
        public static string Remove(this string stringToRemoveFrom, params char[] charsToRemove)
        {
            return Remove(stringToRemoveFrom, (IEnumerable<char>)charsToRemove);
        }

        /// <summary>
        /// Removes a series of characters from a string
        /// </summary>
        /// <param name="stringToRemoveFrom">The string to remove the characters from</param>
        /// <param name="charsToRemove">The characters to remove</param>
        /// <returns></returns>
        public static string Remove(this string stringToRemoveFrom, IEnumerable<char> charsToRemove)
        {
            if (stringToRemoveFrom == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(stringToRemoveFrom)).Create();
            }

            if (charsToRemove == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(charsToRemove)).Create();
            }

            StringBuilder sb = new StringBuilder();

            var charsToRemoveHash = new HashSet<char>(charsToRemove);

            foreach (char c in stringToRemoveFrom)
            {
                if (!charsToRemoveHash.Contains(c))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Removes a string from another string
        /// </summary>
        /// <param name="stringToRemoveFrom">The string to remove from</param>
        /// <param name="textToRemove">The string to remove</param>
        /// <returns></returns>
        public static string Remove(this string stringToRemoveFrom, string textToRemove)
        {
            if (stringToRemoveFrom == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(stringToRemoveFrom)).Create();
            }

            return stringToRemoveFrom.Replace(textToRemove, string.Empty);
        }

        /// <summary>
        /// Removes a series of strings from a string
        /// </summary>
        /// <param name="stringToRemoveFrom">The string to remove the strings from</param>
        /// <param name="textToRemove">The strings to remove</param>
        /// <returns></returns>
        public static string Remove(this string stringToRemoveFrom, params string[] textToRemove)
        {
            return Remove(stringToRemoveFrom, (IEnumerable<string>)textToRemove);
        }

        /// <summary>
        /// Removes a series of strings from a string
        /// </summary>
        /// <param name="stringToRemoveFrom">The string to remove the strings from</param>
        /// <param name="textToRemove">The strings to remove</param>
        /// <returns></returns>
        public static string Remove(this string stringToRemoveFrom, IEnumerable<string> textToRemove)
        {
            if (stringToRemoveFrom == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(stringToRemoveFrom)).Create();
            }

            if (textToRemove == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(textToRemove)).Create();
            }

            string retString = stringToRemoveFrom;

            // TODO Optimise. Currently (o)²
            foreach (var text in textToRemove.OrderByDescending(t => t.Length))
            {
                retString = retString.Replace(text, string.Empty);
            }

            return retString;
        }

        /// <summary>
        /// Converts a string to camel case
        /// </summary>
        /// <param name="stringToConvert">The string to convert</param>
        /// <param name="firstCharIsUpper">Whether the first character should be upper case. Setting this to true is the same as <see cref="ToPascalCase"/></param>
        /// <returns></returns>
        public static string ToCamelCase(this string stringToConvert, bool firstCharIsUpper)
        {
            if (stringToConvert == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(stringToConvert)).Create();
            }

            var wordDelimiters = new[] { ' ', '-', '_' };

            var words = stringToConvert.Split(wordDelimiters, StringSplitOptions.None);

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

                return char.ToUpper(word[0]) + word.Substring(1).ToLower();
            }

            StringBuilder camelCase = new StringBuilder(stringToConvert.Length);

            camelCase.Append(firstCharIsUpper ? MakeWordCamelCase(words[0]) : words[0].ToLower());

            for (int wordIndex = 1; wordIndex < words.Length; ++wordIndex)
            {
                camelCase.Append(MakeWordCamelCase(words[wordIndex]));
            }

            return camelCase.ToString();
        }

        /// <summary>
        /// Converts a string to camel case
        /// </summary>
        /// <param name="stringToConvert">The string to convert</param>
        /// <returns></returns>
        public static string ToPascalCase(this string stringToConvert)
        {
            return ToCamelCase(stringToConvert, true);
        }

        /// <summary>
        /// Converts a camel case or pascal case formatted string to a normal string
        /// </summary>
        /// <param name="stringToConvert">The string to convert</param>
        /// <param name="startEachWordWithCapital">Whether to start each word with a capital letter, such as in a title</param>
        /// <returns></returns>
        public static string FromCamelAndPascalCase(this string stringToConvert, bool startEachWordWithCapital = false)
        {
            if (stringToConvert == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(stringToConvert)).Create();
            }

            if (stringToConvert.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            StringBuilder acronymBuilder = new StringBuilder();
            StringBuilder numberBuilder = new StringBuilder();

            void EmptyAcronymBuilder()
            {
                if (acronymBuilder.Length == 0)
                {
                    // Already empty
                    return;
                }

                var newWordChar = acronymBuilder[acronymBuilder.Length - 1];
                if (startEachWordWithCapital)
                {
                    newWordChar = char.ToUpperInvariant(newWordChar);
                }
                else
                {
                    newWordChar = char.ToLowerInvariant(newWordChar);
                }
                acronymBuilder.Length--;

                if (result.Length > 0)
                {
                    result.Append(" ");
                }

                if (acronymBuilder.Length > 0)
                {
                    // Append acronym
                    string acronym = acronymBuilder.ToString();

                    if (acronym.Length > 1 || startEachWordWithCapital)
                    {
                        result.Append(acronym.ToUpper());
                    }
                    else
                    {
                        // Single letter word
                        result.Append(acronym.ToLower());
                    }

                    result.Append(" ");
                }

                result.Append(newWordChar);

                acronymBuilder.Clear();
            }

            void EmptyNumberBuilder()
            {
                if (numberBuilder.Length > 0)
                {
                    result.Append(" ");
                    result.Append(numberBuilder);

                    numberBuilder.Clear();
                }
            }

            for (int index = 0; index < stringToConvert.Length; index++)
            {
                char curChar = stringToConvert[index];

                if (char.IsNumber(curChar))
                {
                    EmptyAcronymBuilder();

                    numberBuilder.Append(curChar);
                }
                else if (index == 0 || char.IsUpper(curChar) || numberBuilder.Length > 0)
                {
                    EmptyNumberBuilder();

                    acronymBuilder.Append(curChar);
                }
                else
                {
                    EmptyAcronymBuilder();
                    EmptyNumberBuilder();

                    result.Append(curChar);
                }
            }

            EmptyAcronymBuilder();
            EmptyNumberBuilder();

            return result.ToString();
        }
    }
}