namespace MarkSFrancis.AspNetCore.Rest.Models
{
    /// <summary>
    /// An object containing all the hyperlinks related to this object. Each property name is the name of the link, and the value should be the link it points to
    /// </summary>
    public interface ILinksDtoModel
    {
        /// <summary>
        /// The link to this HATEOAS object
        /// </summary>
        string Self { get; }
    }
}
