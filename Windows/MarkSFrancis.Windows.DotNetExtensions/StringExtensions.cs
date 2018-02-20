using System.Net.Mail;

namespace MarkSFrancis.Windows.Extensions
{
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