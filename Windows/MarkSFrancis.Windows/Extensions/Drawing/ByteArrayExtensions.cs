using System.Drawing;
using MarkSFrancis.Collections.Extensions;

namespace MarkSFrancis.Windows.Extensions.Drawing
{
    /// <summary>
    /// Extensions for <see cref="T:byte[]"/>
    /// </summary>
    public static class ByteArrayExtensions
    {
        public static Image ToImage(this byte[] imgAsBytes)
        {
            using (var ms = imgAsBytes.ToMemoryStream())
            {
                return Image.FromStream(ms);
            }
        }
    }
}