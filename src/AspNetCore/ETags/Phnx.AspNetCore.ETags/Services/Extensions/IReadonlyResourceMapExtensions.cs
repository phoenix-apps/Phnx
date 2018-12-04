using System.Collections.Generic;
using System.Linq;

namespace Phnx.AspNetCore.ETags.Services
{
    /// <summary>
    /// Extensions for <see cref="IReadonlyResourceMapService{TDataModel, TDtoModel}"/>
    /// </summary>
    public static class IReadonlyResourceMapExtensions
    {
        /// <summary>
        /// Maps from a collection of data models to a collection of data transfer objects
        /// </summary>
        /// <param name="mapper">The mapper to use</param>
        /// <param name="data">The data models to map</param>
        /// <returns><paramref name="data"/> mapped to a collection of data transfer objects</returns>
        public static IEnumerable<TDtoModel> MapToDto<TDataModel, TDtoModel>(this IReadonlyResourceMapService<TDataModel, TDtoModel> mapper, IEnumerable<TDataModel> data)
        {
            return data.Select(d => mapper.MapToDto(d));
        }
    }
}
