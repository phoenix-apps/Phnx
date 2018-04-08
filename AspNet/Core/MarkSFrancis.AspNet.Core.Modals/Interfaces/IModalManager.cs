using System.Collections.Generic;

namespace MarkSFrancis.AspNet.Core.Modals.Interfaces
{
    public interface IModalManager<TModal> where TModal : IModalViewModel
    {
        List<TModal> Modals { get; }
        void Clear();
    }
}