using MarkSFrancis.AspNetCore.Modals.Interfaces;
using System;

namespace MarkSFrancis.AspNetCore.Modals
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

        /// <summary>
        /// The icon tag's class e.g "fa fa-user-o"
        /// </summary>
        public string IconClass { get; set; }

        /// <summary>
        /// The icon's color
        /// </summary>
        public string IconColor { get; set; }
    }
}