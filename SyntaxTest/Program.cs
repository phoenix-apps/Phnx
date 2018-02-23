using System.Text;
using MarkSFrancis.Console;
using MarkSFrancis.Security;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();

        static void Main(string[] args)
        {
            string plainText = "This is an awkwardly long message that must be secured in a meaningful way";
            AesEncryption aes = new AesEncryption();

            byte[] pass = KeyGen.SecureRandomBytes(32);
            var enc = aes.Encrypt(plainText, pass, Encoding.UTF8);

            var plainEnc = Encoding.UTF8.GetString(enc);

            var dec = aes.Decrypt(enc, pass, Encoding.UTF8);

            Console.WriteLine("Original: " + plainText);
            Console.WriteLine("Encrypted: " + plainEnc);
            Console.WriteLine("Decrypted: " + dec);

            Console.ReadKey();
        }
    }
}
