namespace Phnx.Web
{
    /// <summary>
    /// MIME type helpers
    /// </summary>
    public static class ContentType
    {
        /// <summary>
        /// Specifies the kind of application data
        /// </summary>
        public static class Application
        {
            /// <summary>
            /// Javascript content type (not JSON - for JSON see <see cref="Json"/>)
            /// </summary>
            public const string Javascript = "application/javascript";

            /// <summary>
            /// JSON content type
            /// </summary>
            public const string Json = "application/json";

            /// <summary>
            /// Binary data content type
            /// </summary>
            public const string Octet = "application/octet-stream";

            /// <summary>
            /// PDF content type
            /// </summary>
            public const string Pdf = "application/pdf";

            /// <summary>
            /// Rich text format content type
            /// </summary>
            public const string Rtf = "application/rtf";

            /// <summary>
            /// SOAP document (XML) content type
            /// </summary>
            public const string Soap = "application/soap+xml";

            /// <summary>
            /// ZIP compressed content type
            /// </summary>
            public const string Zip = "application/zip";

            /// <summary>
            /// Form content type
            /// </summary>
            public const string Form = "application/x-www-form-urlencoded";
        }

        /// <summary>
        /// Specifies a type of image data
        /// </summary>
        public static class Image
        {
            /// <summary>
            /// Bitmap image content type
            /// </summary>
            public const string Bmp = "image/bmp";

            /// <summary>
            /// GIF content type
            /// </summary>
            public const string Gif = "image/gif";

            /// <summary>
            /// Icon (.ico) content type
            /// </summary>
            public const string Icon = "image/x-icon";

            /// <summary>
            /// JPEG content type
            /// </summary>
            public const string Jpeg = "image/jpeg";

            /// <summary>
            /// PNG image content type
            /// </summary>
            public const string Png = "image/png";

            /// <summary>
            /// TIFF content type
            /// </summary>
            public const string Tiff = "image/tiff";
        }

        /// <summary>
        /// Specifies a type of text data
        /// </summary>
        public static class Text
        {
            /// <summary>
            /// iCalendar content type
            /// </summary>
            public const string Calendar = "text/calendar";

            /// <summary>
            /// CSS content type
            /// </summary>
            public const string Css = "text/css";

            /// <summary>
            /// CSV content type
            /// </summary>
            public const string Csv = "text/csv	";

            /// <summary>
            /// HTML content type
            /// </summary>
            public const string Html = "text/html";

            /// <summary>
            /// Plain text content type
            /// </summary>
            public const string Plain = "text/plain";

            /// <summary>
            /// Rich text content type
            /// </summary>
            public const string RichText = "text/richtext";

            /// <summary>
            /// XML content type
            /// </summary>
            public const string Xml = "text/xml";
        }
    }
}
