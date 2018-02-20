using System;

namespace MarkSFrancis.AspNet.Windows
{
    public static class ErrorFactory
    {
        public static NullReferenceException HttpContextRequired => new NullReferenceException("A http context is required");
    }
}
