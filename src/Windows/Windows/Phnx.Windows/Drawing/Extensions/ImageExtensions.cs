using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Phnx.Drawing
{
    /// <summary>
    /// Extensions for <see cref="Image"/>
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Stretch an image to a width and height
        /// </summary>
        /// <param name="img">The iamge to stretch</param>
        /// <param name="width">The width to stretch to</param>
        /// <param name="height">The height to stretch to</param>
        /// <param name="highQuality">Whether to use higher quality scaling</param>
        /// <returns>The image stretched to a width and height</returns>
        public static Image Scale(this Image img, int width, int height, bool highQuality = false)
        {
            var stretchedImage = new Bitmap(width, height);

            using (Graphics stretchedImageGraphics = Graphics.FromImage(stretchedImage))
            {
                if (highQuality)
                {
                    stretchedImageGraphics.CompositingQuality = CompositingQuality.HighQuality;
                    stretchedImageGraphics.SmoothingMode = SmoothingMode.HighQuality;
                    stretchedImageGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                }

                stretchedImageGraphics.DrawImage(img, 0, 0, width, height);
            }

            return stretchedImage;
        }

        /// <summary>
        /// Scale an image to a width and height, maintaining the image's original aspect ratio
        /// </summary>
        /// <param name="img">The image to scale</param>
        /// <param name="width">The maximum width to scale to</param>
        /// <param name="height">The maximum height to scale to</param>
        /// <param name="background">The color to fill the empty space around the edge with</param>
        /// <param name="highQuality">Whether to use higher quality scaling</param>
        /// <returns>The image scaled to a width and height</returns>
        public static Image Scale(this Image img, int width, int height, Color background, bool highQuality = false)
        {
            var scaledImage = new Bitmap(width, height);

            var backgroundBrush = new SolidBrush(background);

            // Calculate the scale that is allowed by the width and height
            float scale = Math.Min(width / (float)img.Width, height / (float)img.Height);

            using (var scaledImageGraphics = Graphics.FromImage(scaledImage))
            {
                if (highQuality)
                {
                    scaledImageGraphics.InterpolationMode = InterpolationMode.High;
                    scaledImageGraphics.CompositingQuality = CompositingQuality.HighQuality;
                    scaledImageGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                }

                // Calculate the image's scaled width and height
                var scaleWidth = (int)(img.Width * scale);
                var scaleHeight = (int)(img.Height * scale);

                // Fill with the background color
                scaledImageGraphics.FillRectangle(backgroundBrush, new RectangleF(0, 0, width, height));

                // Fill the largest space allowed by the image's aspect ratio with the image
                scaledImageGraphics.DrawImage(img, new Rectangle((width - scaleWidth) / 2, (height - scaleHeight) / 2, scaleWidth,
                        scaleHeight));

                return scaledImage;
            }
        }
    }
}