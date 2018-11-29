using System;

namespace Phnx.AspNet.Modals
{
    /// <summary>
    /// A view model describing a modal dialog
    /// </summary>
    public class ModalViewModel : IModalViewModel
    {
        /// <summary>
        /// Create a new <see cref="ModalViewModel"/> with a random <see cref="Guid"/> as the <see cref="Id"/>
        /// </summary>
        public ModalViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// The unique ID for this modal dialog
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The heading for the modal dialog
        /// </summary>
        public string Heading { get; set; }

        /// <summary>
        /// The body content for the modal dialog
        /// </summary>
        public string Body { get; set; }
    }
}