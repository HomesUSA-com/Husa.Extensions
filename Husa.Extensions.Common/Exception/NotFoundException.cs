namespace Husa.Extensions.Common.Exception
{
    using Husa.Extensions.Common.Exception.Filters;

#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class NotFoundException<T> : NotFoundException
    {
        public NotFoundException(object id)
        {
            this.Id = id;
        }

        public object Id { get; }

        public override string Message => $"The Id '{this.Id}' wasn't found for the type '{typeof(T)}'";
    }
}
