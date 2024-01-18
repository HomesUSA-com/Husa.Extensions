namespace Husa.Extensions.Common.Exceptions
{
    using System;
#if !NET8_0_OR_GREATER
    using System.Runtime.Serialization;
#endif

#if !NET8_0_OR_GREATER
    [Serializable]
#endif
    public class DomainException : Exception
    {
        public DomainException()
        {
        }

        public DomainException(string message)
            : base(message)
        {
        }

        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !NET8_0_OR_GREATER
        protected DomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
