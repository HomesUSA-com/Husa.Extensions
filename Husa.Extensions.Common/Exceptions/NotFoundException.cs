namespace Husa.Extensions.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

#pragma warning disable SA1402 // File may only contain a single type
    [Serializable]
    public sealed class NotFoundException<T> : NotFoundException
    {
        public NotFoundException(object id)
        {
            this.Id = id;
        }

        private NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public object Id { get; }

        public override string Message => $"The Id '{this.Id}' wasn't found for the type '{typeof(T)}'";
    }

    [Serializable]
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException()
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
