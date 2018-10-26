using System;
using System.IO;

namespace Phnx.Security.Algorithms
{
    /// <summary>
    /// A helper implementation of <see cref="ISymmetricEncryption"/> for using <see cref="T:byte[]"/> instead of <see cref="Stream"/> for output, and other overloads
    /// </summary>
    public abstract class SymmetricEncryption : ISymmetricEncryption
    {
        /// <summary>
        /// The length of the marker for the IV size used by <see cref="Encrypt(byte[], byte[])"/> and <see cref="Decrypt(byte[], byte[])"/> when storing the IV with the encrypted data
        /// </summary>
        public const int IvLengthMarkerBits = 32;

        /// <summary>
        /// Generate a random secure IV of a length that is best suited for this algorithm
        /// </summary>
        /// <returns>A random secure IV</returns>
        public abstract byte[] CreateRandomIv();

        /// <summary>
        /// Generate a random secure key of a length that is best suited for this algorithm
        /// </summary>
        /// <returns>A random secure key</returns>
        public abstract byte[] CreateRandomKey();

        /// <summary>
        /// Decrypt data using a key
        /// </summary>
        /// <param name="input">The stream which contains the data to decrypt</param>
        /// <param name="key">The key to use when decrypting the data</param>
        /// <param name="iv">The initialisation vector to use when decrypting the data</param>
        /// <param name="output">Where to output the decrypted bytes</param>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> or <paramref name="key"/> or <paramref name="iv"/> or <paramref name="output"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="input"/> does not support read or <paramref name="output"/> does not support write</exception>
        public abstract void Decrypt(Stream input, byte[] key, byte[] iv, Stream output);

        /// <summary>
        /// Encrypt data using a key
        /// </summary>
        /// <param name="input">The stream which contains the data to encrypt</param>
        /// <param name="key">The key to use when encrypting the data</param>
        /// <param name="iv">The initialisation vector to use when encrypting the data</param>
        /// <param name="output">Where to output the encrypted bytes</param>
        /// <returns><paramref name="input"/> encrypted</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> or <paramref name="key"/> or <paramref name="iv"/> or <paramref name="output"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="input"/> does not support read or <paramref name="output"/> does not support write</exception>
        public abstract void Encrypt(Stream input, byte[] key, byte[] iv, Stream output);

        /// <summary>
        /// Generate a random secure key of length <paramref name="keySizeBytes"/> in bytes
        /// </summary>
        /// <param name="keySizeBytes"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentLessThanZeroException"><paramref name="keySizeBytes"/> is less than zero</exception>
        public byte[] CreateRandomKey(int keySizeBytes)
        {
            if (keySizeBytes < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(keySizeBytes));
            }

            return SecureRandomBytes.Generate(keySizeBytes);
        }

        /// <summary>
        /// Generate a random secury IV of length <paramref name="ivSizeBytes"/>
        /// </summary>
        /// <returns>A random secury IV of length <paramref name="ivSizeBytes"/></returns>
        public byte[] CreateRandomIv(int ivSizeBytes)
        {
            if (ivSizeBytes < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(ivSizeBytes));
            }

            return SecureRandomBytes.Generate(ivSizeBytes);
        }

        /// <summary>
        /// Saves the IV's length and IV
        /// </summary>
        /// <param name="iv">The IV to save</param>
        /// <param name="output">The stream to save to</param>
        public void WriteIvHeader(byte[] iv, Stream output)
        {
            var ivLength = BitConverter.GetBytes(iv.Length);
            output.Write(ivLength, 0, ivLength.Length);
            output.Write(iv, 0, iv.Length);
        }

        /// <summary>
        /// Reads the IV's length and IV
        /// </summary>
        /// <param name="input">The stream to read from</param>
        /// <returns>The IV read from <paramref name="input"/></returns>
        public byte[] ReadIvHeader(byte[] input)
        {
            var ivLength = new byte[IvLengthMarkerBits / 8];
            Array.Copy(input, ivLength, ivLength.Length);

            var iv = new byte[BitConverter.ToInt32(ivLength, 0)];

            Array.Copy(input, IvLengthMarkerBits / 8, iv, 0, iv.Length);

            return iv;
        }

        /// <summary>
        /// Encrypt data using a key. The randomly generated IV is stored at the beginning of the returned data ready to be used when you decrypt the data
        /// </summary>
        /// <param name="data">The data to encrypt</param>
        /// <param name="key">The key to use when encrypting the data</param>
        /// <returns><paramref name="data"/> encrypted, with the size of the IV stored in the first <see cref="IvLengthMarkerBits"/> bits, then the IV, then the encrypted data</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="key"/> is <see langword="null"/></exception>
        public byte[] Encrypt(byte[] data, byte[] key)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            byte[] iv = CreateRandomIv();

            var output = new MemoryStream(IvLengthMarkerBits / 8 + iv.Length);
            WriteIvHeader(iv, output);

            var input = new MemoryStream(data);
            Encrypt(input, key, iv, output);

            return output.ToArray();
        }

        /// <summary>
        /// Encrypt data using a key
        /// </summary>
        /// <param name="data">The data to encrypt</param>
        /// <param name="key">The key to use when encrypting the data</param>
        /// <param name="iv">The initialisation vector to use when encrypting the data</param>
        /// <returns><paramref name="data"/> encrypted</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="key"/> or <paramref name="iv"/> is <see langword="null"/></exception>
        public byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            return Encrypt(data, 0, data.Length, key, iv);
        }

        /// <summary>
        /// Encrypt data using a key
        /// </summary>
        /// <param name="sourceBuffer">The data buffer which contains the data to encrypt</param>
        /// <param name="sourceStartIndex">The index in <paramref name="sourceBuffer"/> at which the data to encrypt begins</param>
        /// <param name="bytesToEncrypt">The length of the data in <paramref name="sourceBuffer"/> to encrypt</param>
        /// <param name="key">The key to use when encrypting the data</param>
        /// <param name="iv">The initialisation vector to use when encrypting the data</param>
        /// <returns><paramref name="sourceBuffer"/> encrypted</returns>
        /// <exception cref="ArgumentNullException"><paramref name="sourceBuffer"/> or <paramref name="key"/> or <paramref name="iv"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentLessThanZeroException"><paramref name="sourceStartIndex"/> or <paramref name="bytesToEncrypt"/> is less than zero</exception>
        /// <exception cref="ArgumentOutOfRangeException">The sum of <paramref name="bytesToEncrypt"/> and <paramref name="sourceStartIndex"/> is greater than the length of <paramref name="sourceBuffer"/></exception>
        public byte[] Encrypt(byte[] sourceBuffer, int sourceStartIndex, int bytesToEncrypt, byte[] key, byte[] iv)
        {
            var output = new MemoryStream();
            var input = new MemoryStream(sourceBuffer, sourceStartIndex, bytesToEncrypt);

            Encrypt(input, key, iv, output);

            return output.ToArray();
        }

        /// <summary>
        /// Decrypt data using a key. The IV's size must be stored in the first <see cref="IvLengthMarkerBits"/>, followed by the randomly generated IV, and then the encrypted data, which is done automatically for you by <see cref="Encrypt(byte[], byte[])"/>
        /// </summary>
        /// <param name="encryptedData">The data to decrypt</param>
        /// <param name="key">The key to use when decrypting the data</param>
        /// <returns><paramref name="encryptedData"/> decrypted</returns>
        /// <exception cref="ArgumentNullException"><paramref name="encryptedData"/> or <paramref name="key"/> is <see langword="null"/></exception>
        public byte[] Decrypt(byte[] encryptedData, byte[] key)
        {
            if (encryptedData is null)
            {
                throw new ArgumentNullException(nameof(encryptedData));
            }

            // Extract IV size
            int ivSize = BitConverter.ToInt32(encryptedData, 0);
            var ivLengthMarkerBytes = IvLengthMarkerBits / 8;

            // Extract IV
            var iv = new byte[ivSize];
            Array.Copy(encryptedData, ivLengthMarkerBytes, iv, 0, iv.Length);

            return Decrypt(encryptedData, ivLengthMarkerBytes + iv.Length, encryptedData.Length - (ivLengthMarkerBytes + iv.Length), key, iv);
        }

        /// <summary>
        /// Decrypt using a specified IV
        /// </summary>
        /// <param name="encryptedData">The encrypted data to decrypt</param>
        /// <param name="key">The key to decrypt with</param>
        /// <param name="iv">The initialisation vector to decrypt with</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="encryptedData"/> or <paramref name="key"/> or <paramref name="iv"/> is <see langword="null"/></exception>
        public byte[] Decrypt(byte[] encryptedData, byte[] key, byte[] iv)
        {
            if (encryptedData is null)
            {
                throw new ArgumentNullException(nameof(encryptedData));
            }

            return Decrypt(encryptedData, 0, encryptedData.Length, key, iv);
        }

        /// <summary>
        /// Decrypt data using a key
        /// </summary>
        /// <param name="encryptedDataBuffer">The data buffer which contains the data to decrypt</param>
        /// <param name="dataStartIndex">The index in <paramref name="encryptedDataBuffer"/> at which the data to decrypt begins</param>
        /// <param name="dataLength">The length of the data in <paramref name="encryptedDataBuffer"/> to decrypt</param>
        /// <param name="key">The key to use when decrypting the data</param>
        /// <param name="iv">The initialisation vector to use when decrypting the data</param>
        /// <exception cref="ArgumentNullException"><paramref name="encryptedDataBuffer"/> or <paramref name="key"/> or <paramref name="iv"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentLessThanZeroException"><paramref name="dataStartIndex"/> or <paramref name="dataLength"/> is less than zero</exception>
        /// <exception cref="ArgumentOutOfRangeException">The sum of <paramref name="dataLength"/> and <paramref name="dataStartIndex"/> is greater than the length of <paramref name="encryptedDataBuffer"/></exception>
        public byte[] Decrypt(byte[] encryptedDataBuffer, int dataStartIndex, int dataLength, byte[] key, byte[] iv)
        {
            var output = new MemoryStream();
            var input = new MemoryStream(encryptedDataBuffer, dataStartIndex, dataLength);

            Decrypt(input, key, iv, output);

            return output.ToArray();
        }
    }
}
