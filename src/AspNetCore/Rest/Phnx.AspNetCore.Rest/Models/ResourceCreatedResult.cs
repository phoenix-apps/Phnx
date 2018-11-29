namespace Phnx.AspNet.Core.Rest.Models
{
    public class ResourceCreatedResult
    {
        private ResourceCreatedResult()
        {
        }

        internal bool ByUrl { get; set; }

        internal string Url { get; set; }

        internal string ActionName { get; set; }

        internal string ControllerName { get; set; }

        internal object RouteValues { get; set; }

        /// <summary>
        /// The object was successfully created at the specified URL
        /// </summary>
        /// <param name="url">The URL at which the object was created</param>
        /// <returns>A new <see cref="ResourceCreatedResult"/> that contains the URL at which the resource was created</returns>
        public ResourceCreatedResult Created(string url)
        {
            return new ResourceCreatedResult
            {
                Url = url,
                ByUrl = true
            };
        }

        /// <summary>
        /// The object was successfully created at the specified path
        /// </summary>
        /// <param name="actionName">The name of the action for accessing the resource</param>
        /// <param name="controllerName">The name of the controller for accessing the resource</param>
        /// <param name="routeValues">The route values needed to access the resource</param>
        /// <returns>A new <see cref="ResourceCreatedResult"/> that contains the URL at which the resource was created</returns>
        public ResourceCreatedResult Created(string actionName, string controllerName, object routeValues)
        {
            return new ResourceCreatedResult
            {
                ActionName = actionName,
                ControllerName = controllerName,
                RouteValues = routeValues,
                ByUrl = false
            };
        }
    }
}
