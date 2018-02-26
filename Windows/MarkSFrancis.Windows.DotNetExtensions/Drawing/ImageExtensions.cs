using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MarkSFrancis.Windows.Extensions.Drawing
{
    /// <summary>
    /// Extensions for <see cref="Image"/>
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Scale and stretch an image to a specific with and height
        /// </summary>
        /// <param name="img">The @this to act on.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="highQuality">Whether to use higher quality scaling</param>
        /// <returns>The scaled image to the specific width and height.</returns>
        public static Image Scale(this Image img, int width, int height, bool highQuality = false)
        {
            var scaledImage = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                if (highQuality)
                {
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                }

                g.DrawImage(img, 0, 0, width, height);
            }

            return scaledImage;
        }

        /// <summary>
        /// Scale an image to a specified width and height. Maintains the image's original aspect ratio
        /// </summary>
        /// <param name="img">The image to scale</param>
        /// <param name="width">The output width</param>
        /// <param name="height">The output height</param>
        /// <param name="background">The color to fill the empty space around the edge with</param>
        /// <param name="highQuality">Whether to use higher quality scaling</param>
        /// <returns>The scaled image to the specific width and height.</returns>
        public static Image Scale(this Image img, int width, int height, Color background, bool highQuality = false)
        {
            var scaledImage = new Bitmap(width, height);

            var brush = new SolidBrush(background);
            float scale = Math.Min(width / (float)img.Width, height / (float)img.Height);
            
            using (var graph = Graphics.FromImage(scaledImage))
            {
                if (highQuality)
                {
                    graph.InterpolationMode = InterpolationMode.High;
                    graph.CompositingQuality = CompositingQuality.HighQuality;
                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                }

                var scaleWidth = (int) (img.Width * scale);
                var scaleHeight = (int) (img.Height * scale);

                graph.FillRectangle(brush, new RectangleF(0, 0, width, height));
                graph.DrawImage(img, new Rectangle((width - scaleWidth) / 2, (height - scaleHeight) / 2, scaleWidth,
                        scaleHeight));

                return scaledImage;
            }
        }
    }
}