using Microsoft.AspNetCore.Mvc;

namespace Phnx.AspNetCore.ETags.Services
{
    public interface IETagResponseService
    {
        IETagService ETagService { get; }

        void AddBestETagToResponse(object data);
        void AddWeakETagToResponse(object data);
        StatusCodeResult CreateDataHasChangedResponse();
        StatusCodeResult CreateDataHasNotChangedResponse();
        bool TryAddStrongETagToResponse(object data);
    }
}