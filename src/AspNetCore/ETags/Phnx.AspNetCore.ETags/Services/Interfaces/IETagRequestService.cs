namespace Phnx.AspNetCore.ETags.Services
{
    public interface IETagRequestService
    {
        bool ShouldDelete(object data);
        bool ShouldGetSingle(object savedData);
        bool ShouldUpdate(object savedData);
    }
}