using System;
using System.Runtime.Serialization;

namespace Dal
{
    [Serializable]
    internal class DuplicateIdException : Exception
    {
        private string v;
        private object guestRequestKey;

        public DuplicateIdException()
        {
        }

        public DuplicateIdException(string message) : base(message)
        {
        }

        public DuplicateIdException(string v, object guestRequestKey)
        {
            this.v = v;
            this.guestRequestKey = guestRequestKey;
        }

        public DuplicateIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}