using MarkSFrancis.AspNet.Core.Modals.Interfaces;

namespace MarkSFrancis.AspNet.Core.Modals
{
    public class ModalModel : IModalModel
    {
        public ModalModel()
        {
            Id = System.Guid.NewGuid().ToString();
        }

        public string Id { get; }

        public string Heading { get; set; }

        public string Body { get; set; }

        /// <summary>
        /// The icon tag's class e.g "fa fa-user-o"
        /// </summary>
        public string IconClass { get; set; }

        public string IconColor { get; set; }

        public virtual string IconHtml
        {
            get
            {
                if (string.IsNullOrWhiteSpace(IconClass))
                {
                    return "";
                }

                string icon = "<i class=\"" + IconClass + "\"";

                if (!string.IsNullOrWhiteSpace(IconColor))
                {
                    icon += " style=\"color:" + IconColor + ";\"";
                }

                return icon + "></i>";
            }
        }
    }
}