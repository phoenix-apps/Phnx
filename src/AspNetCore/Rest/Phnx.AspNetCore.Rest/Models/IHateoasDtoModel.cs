namespace Phnx.AspNetCore.Rest.Models
{
    /// <summary>
    /// A HATEOAS compliant data transfer object, with a series of links contains within a <typeparamref name="TLinks"/> object
    /// </summary>
    /// <typeparam name="TLinks"></typeparam>
    public interface IHateoasDtoModel<TLinks> where TLinks : ILinksDtoModel
    {
        /// <summary>
        /// The HATEOAS links for this object, including a link to itself, and related objects
        /// </summary>
        TLinks Links { get; set; }
    }
}
