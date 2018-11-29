namespace Phnx.AspNet.Modals
{
    /// <summary>
    /// A view model describing a modal dialog
    /// </summary>
    public interface IModalViewModel
    {
        /// <summary>
        /// The unique ID for this modal
        /// </summary>
        string Id { get; }
    }
}
