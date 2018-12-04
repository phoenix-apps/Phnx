namespace Phnx.AspNet.Core.Rest.Models
{
    /// <summary>
    /// Captures information about the route/ URL at which a resource was created
    /// </summary>
    public class ResourceCreatedResult
    {
        /// <summary>
        /// The object was successfully created at the specified URL
        /// </summary>
        /// <param name="url">The URL at which the object was created</param>
        public ResourceCreatedResult(string url)
        {
            ByUrl = true;
            Url = url;
        }

        /// <summary>
        /// The object was successfully created at the specified path
        /// </summary>
        /// <param name="actionName">The name of the action for accessing the resource</param>
        /// <param name="controllerName">The name of the controller for accessing the resource</param>
        /// <param name="routeValues">The route values needed to access the resource</param>
        public ResourceCreatedResult(string actionName, string controllerName, object routeValues)
        {
            ByUrl = false;
            ActionName = actionName;
            ControllerName = controllerName;
            RouteValues = routeValues;
        }

        /// <summary>
        /// Whether the URL is tracked by an absolute <see cref="Url"/> (<see langword="true"/>), or by an <see cref="ActionName"/>, <see cref="ControllerName"/> and <see cref="RouteValues"/> combination (<see langword="false"/>)
        /// </summary>
        public bool ByUrl { get; }

        /// <summary>
        /// The absolute URL to the location at which the resource was created
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// The name of the action at which the item was created
        /// </summary>
        public string ActionName { get; }

        /// <summary>
        /// The name of the controller at which the item was created
        /// </summary>
        public string ControllerName { get; }

        /// <summary>
        /// Route values for the route at which the item was created
        /// </summary>
        public object RouteValues { get; }
    }
}
