using System.Collections.Generic;

namespace Phnx.AspNetCore.Modals.Interfaces
{
    /// <summary>
    /// A manager hosting and retrieving a list of all the modals to render
    /// </summary>
    /// <typeparam name="TModal">The type of modals that this manager hosts</typeparam>
    public interface IModalManager<TModal> where TModal : IModalViewModel
    {
        /// <summary>
        /// Get all the modals currently stored in this session
        /// </summary>
        List<TModal> Get();

        /// <summary>
        /// Set all the modals currently stored in this session
        /// </summary>
        void Set(List<TModal> modals);

        /// <summary>
        /// Clear all the modals from the current session
        /// </summary>
        void Clear();
    }
}