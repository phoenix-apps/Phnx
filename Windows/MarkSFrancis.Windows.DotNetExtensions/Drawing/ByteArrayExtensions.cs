using System.Drawing;
using System.IO;

namespace MarkSFrancis.Windows.Extensions.Drawing
{
    /// <summary>
    /// Extensions for <see cref="T:byte[]"/>
    /// </summary>
    public static class ByteArrayExtensions
    {
        public static Image ToImage(this byte[] imgAsBytes)
        {
            using (var ms = new MemoryStream(imgAsBytes))
            {
                return Image.FromStream(ms);
            }
        }
    }
}