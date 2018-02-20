using System.Web;

namespace MarkSFrancis.AspNet.Windows.Modals
{
    /// <summary>
    /// The default message view modal, which has a dependancy on Bootstrap 4 and Font Awesome 5. If you want a custom implementation, create an implementation of IModal
    /// </summary>
    public class ModalViewModal : IModal
    {
        public ModalViewModal()
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        public string Guid { get; set; }

        public string Heading { get; set; }

        public string Body { get; set; }

        /// <summary>
        /// The font awesome class e.g "fa fa-user-o"
        /// </summary>
        public string FontAwesomeIcon { get; set; }

        public string IconColor { get; set; }

        public string FontAwesomeIconHtml
        {
            get
            {
                if (string.IsNullOrWhiteSpace(FontAwesomeIcon))
                {
                    return "";
                }

                string icon = "<i class=\"" + FontAwesomeIcon + "\"";

                if (!string.IsNullOrWhiteSpace(IconColor))
                {
                    icon += " style=\"color:" + IconColor + ";\"";
                }

                return icon + "></i>";
            }
        }
    }
}