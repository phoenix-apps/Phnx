using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace Phnx.AspNet.Modals
{
    /// <summary>
    /// A manager hosting and retrieving a list of all the modals to render
    /// </summary>
    /// <typeparam name="TModal">The type of modals that this manager hosts</typeparam>
    public class ModalManager<TModal> : IModalManager<TModal> where TModal : IModalViewModel
    {
        /// <summary>
        /// The current <see cref="HttpContext.Session"/>
        /// </summary>
        public HttpSessionState Session => HttpContext.Current?.Session;

        /// <summary>
        /// The key to the part of <see cref="Session"/> which contains all the modal data
        /// </summary>
        public const string SessionMessageKey = "Modals";

        /// <summary>
        /// Get all the modals currently stored in this session
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Session"/> is <see langword="null"/></exception>
        public List<TModal> Get()
        {
            if (Session is null)
            {
                throw new InvalidOperationException($"{nameof(HttpContext)} is required");
            }

            return (List<TModal>)Session[SessionMessageKey];
        }

        /// <summary>
        /// Set all the modals currently stored in this session
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Session"/> is <see langword="null"/></exception>
        public void Set(List<TModal> modals)
        {
            if (Session is null)
            {
                throw new InvalidOperationException($"{nameof(HttpContext)} is required");
            }

            Session[SessionMessageKey] = modals;
        }

        /// <summary>
        /// Clear all the modals from the current session
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Session"/> is <see langword="null"/></exception>
        public void Clear()
        {
            Set(null);
        }
    }
}