namespace Husa.Extensions.Common.Exceptions
{
    using System;
#if !NET8_0_OR_GREATER
    using System.Runtime.Serialization;
#endif

#pragma warning disable SA1402 // File may only contain a single type
#if !NET8_0_OR_GREATER
    [Serializable]
#endif
    public sealed class NotFoundException<T> : NotFoundException
    {
        public NotFoundException(object id)
        {
            this.Id = id;
        }

#if !NET8_0_OR_GREATER
        private NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif

        public object Id { get; }

        public override string Message => $"The Id '{this.Id}' wasn't found for the type '{typeof(T)}'";
    }

#if !NET8_0_OR_GREATER
    [Serializable]
#endif
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException()
        {
        }

#if !NET8_0_OR_GREATER
        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
