using System.Collections.Generic;
using MarkSFrancis.AspNet.Core.Context.Interfaces;
using MarkSFrancis.AspNet.Core.Modals.Interfaces;

namespace MarkSFrancis.AspNet.Core.Modals
{
    public class ModalManager<TModal> : IModalManager<TModal> where TModal : IModalViewModel
    {
        private readonly ISessionService _session;
        public const string SessionMessageKey = "ModalMessage";

        public ModalManager(ISessionService session)
        {
            _session = session;
        }

        public List<TModal> Modals
        {
            get => _session.Get<List<TModal>>(SessionMessageKey);
            private set => _session.Set(SessionMessageKey, value);
        }

        public void Clear()
        {
            Modals = null;
        }
    }
}
