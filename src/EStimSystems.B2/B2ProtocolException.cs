using System;
using System.Runtime.Serialization;

namespace EStimSystems.B2
{
    [Serializable]
    public class B2ProtocolException : Exception
    {

        public B2ProtocolException()
        {
        }

        public B2ProtocolException(string message) : base(message)
        {
        }

        public B2ProtocolException(string message, Exception inner) : base(message, inner)
        {
        }

        protected B2ProtocolException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}