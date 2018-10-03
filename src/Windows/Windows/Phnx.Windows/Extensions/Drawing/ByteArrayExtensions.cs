using Phnx.Collections.Extensions;
using System.Drawing;

namespace Phnx.Windows.Extensions.Drawing
{
    /// <summary>
    /// Extensions for <see cref="T:byte[]"/>
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Create an image from a <see cref="T:byte[]"/>
        /// </summary>
        /// <param name="imgAsBytes">The image in byte format</param>
        /// <returns></returns>
        public static Image ToImage(this byte[] imgAsBytes)
        {
            using (var ms = imgAsBytes.ToMemoryStream())
            {
                return Image.FromStream(ms);
            }
        }
    }
}