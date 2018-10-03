using Phnx.AspNet.Context.Interfaces;
using Phnx.AspNet.Modals.Interfaces;
using System.Collections.Generic;

namespace Phnx.AspNet.Modals
{
    /// <summary>
    /// A manager hosting and retrieving a list of all the modals to render
    /// </summary>
    /// <typeparam name="TModal">The type of modals that this manager hosts</typeparam>
    public class ModalManager<TModal> : IModalManager<TModal> where TModal : IModalViewModel
    {
        private readonly ISessionService _session;

        /// <summary>
        /// The key to the part of the session which contains all the modal data
        /// </summary>
        public const string SessionMessageKey = "ModalMessage";

        /// <summary>
        /// Create a new <see cref="ModalManager{TModal}"/> using a <see cref="ISessionService"/> to hold the modal data
        /// </summary>
        /// <param name="session">The session storage in which all modals are stored</param>
        public ModalManager(ISessionService session)
        {
            _session = session;
        }

        /// <summary>
        /// Get or set all the modals currently stored in this session
        /// </summary>
        public List<TModal> Modals
        {
            get => _session.Get<List<TModal>>(SessionMessageKey);
            private set => _session.Set(SessionMessageKey, value);
        }

        /// <summary>
        /// Clear all the modals from the current session
        /// </summary>
        public void Clear()
        {
            Modals = null;
        }
    }
}