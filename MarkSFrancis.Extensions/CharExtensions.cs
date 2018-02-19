using System.Collections.Generic;
using System.Linq;

namespace MarkSFrancis.Extensions
{
    public static class CharExtensions
    {
        public static string ToString(this char c, int repeat)
        {
            return new string(c, repeat);
        }

        public static IEnumerable<char> To(this char startChar, char toChar)
        {
            bool reverseRequired = (startChar > toChar);

            char first = reverseRequired ? toChar : startChar;
            char last = reverseRequired ? startChar : toChar;

            IEnumerable<char> result = Enumerable.Range(first, last - first + 1).Select(charCode => (char)charCode);

            if (reverseRequired)
            {
                result = result.Reverse();
            }
            
            return result;
        }
    }
}