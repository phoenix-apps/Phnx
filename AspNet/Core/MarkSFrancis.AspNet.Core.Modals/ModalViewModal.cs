using MarkSFrancis.AspNet.Core.Modals.Interfaces;

namespace MarkSFrancis.AspNet.Core.Modals
{
    public class ModalViewModel : IModalViewModel
    {
        public ModalViewModel()
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
    }
}