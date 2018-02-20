using System.Collections.Generic;

namespace MarkSFrancis.AspNet.Windows.Services.Cache
{
    public interface ICacheEntryGroup
    {
        IEnumerable<KeyValuePair<ICacheEntryGroupChild, object>> LoadAllFromExternalSource();
    }
}