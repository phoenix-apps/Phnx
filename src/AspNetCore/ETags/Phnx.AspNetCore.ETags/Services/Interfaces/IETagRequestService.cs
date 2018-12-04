namespace Phnx.AspNetCore.ETags.Services
{
    public interface IETagRequestService
    {
        IETagService ETagService { get; }

        bool ShouldDelete(object data);
        bool ShouldGetSingle(object data);
        bool ShouldUpdate(object data);
    }
}