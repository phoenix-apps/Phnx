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
            RsaEncryption rsa = new RsaEncryption();
            rsa.CreateRandomKeys(out byte[] pubKey, out byte[] privKey);

            var encBytes = rsa.Encrypt(plainText, pubKey, Encoding.UTF8);

            var encText = Encoding.UTF8.GetString(encBytes);

            var decText = rsa.Decrypt(encBytes, privKey, Encoding.UTF8);

            Console.WriteLine("Original: " + plainText);

            Console.WriteLine("Encrypted:" + encText);

            Console.WriteLine("Decrypted: " + decText);

            Console.ReadKey();
        }
    }
}
