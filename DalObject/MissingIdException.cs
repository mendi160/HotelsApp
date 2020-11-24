using System;
using System.Runtime.Serialization;

namespace Dal
{
    [Serializable]
    internal class MissingIdException : Exception
    {
        private string v;
        private int id;

        public MissingIdException()
        {
        }

        public MissingIdException(string message) : base(message)
        {
        }

        public MissingIdException(string v, int id)
        {
            this.v = v;
            this.id = id;
        }

        public MissingIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}