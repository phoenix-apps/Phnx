using System.Net.Mail;

namespace MarkSFrancis.Windows.Extensions
{
    /// <summary>
    /// Extensions for <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {

        public static bool IsEmail(this string str)
        {
            try
            {
                var addr = new MailAddress(str);
                return addr.Address == str;
            }
            catch
            {
                return false;
            }
        }
    }
}