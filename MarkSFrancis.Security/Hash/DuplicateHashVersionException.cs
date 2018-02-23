using System;

namespace MarkSFrancis.Security.Hash
{

    [Serializable]
    public class DuplicateHashVersionException : InvalidOperationException
    {
        public DuplicateHashVersionException()
        {
        }

        public DuplicateHashVersionException(string message) : base(message)
        {
        }

        public DuplicateHashVersionException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DuplicateHashVersionException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
