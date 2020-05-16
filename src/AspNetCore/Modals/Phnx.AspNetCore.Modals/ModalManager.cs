using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace Phnx.AspNetCore.Modals
{
    /// <summary>
    /// A manager hosting and retrieving a list of all the modals to render
    /// </summary>
    /// <typeparam name="TModal">The type of modals that this manager hosts</typeparam>
    public class ModalManager<TModal> : IModalManager<TModal> where TModal : IModalViewModel
    {
        /// <summary>
        /// The current <see cref="ISession"/> to store and load modals
        /// </summary>
        public ISession Session => ContextAccessor.HttpContext?.Session;

        /// <summary>
        /// The <see cref="IHttpContextAccessor"/> to load the current <see cref="Session"/> from
        /// </summary>
        public IHttpContextAccessor ContextAccessor { get; }

        /// <summary>
        /// The key to the part of the session which contains all the modal data
        /// </summary>
        public const string SessionModalsKey = "Modals";

        /// <summary>
        /// Create a new <see cref="ModalManager{TModal}"/> using the <see cref="ISession"/> to hold the modal data
        /// </summary>
        /// <param name="contextAccessor">The session storage in which all modals are stored</param>
        public ModalManager(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;
        }

        /// <summary>
        /// Get all the modals currently stored in this session
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Session"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidCastException">Data stored in session with key <see cref="SessionModalsKey"/> is not of type <see cref="List{TModal}"/></exception>
        /// <exception cref="SerializationException">Data stored in session with key <see cref="SessionModalsKey"/> is not a valid serialized data structure</exception>
        public List<TModal> Get()
        {
            if (Session is null)
            {
                throw new InvalidOperationException("A HttpContext is required");
            }

            if (!Session.TryGetValue(SessionModalsKey, out var valueBytes) || valueBytes is null)
            {
                return new List<TModal>();
            }

            var valueString = Encoding.UTF8.GetString(valueBytes);

            var value = JsonSerializer.Deserialize<List<TModal>>(valueString);

            return value;
        }

        /// <summary>
        /// Set all the modals currently stored in this session
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Session"/> is <see langword="null"/></exception>
        public void Set(List<TModal> modals)
        {
            if (Session is null)
            {
                throw new InvalidOperationException("A HttpContext is required");
            }

            if (modals is null || modals.Count == 0)
            {
                Session.Remove(SessionModalsKey);
                return;
            }

            var valueString = JsonSerializer.Serialize(modals);

            var valueBytes = Encoding.UTF8.GetBytes(valueString);

            Session.Set(SessionModalsKey, valueBytes);
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
