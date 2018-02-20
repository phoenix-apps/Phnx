using System.Drawing;
using System.IO;

namespace MarkSFrancis.Windows.Extensions.Drawing
{
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