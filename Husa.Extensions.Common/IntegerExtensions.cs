namespace Husa.Extensions.Common
{
    using System;

    public static class IntegerExtensions
    {
        public static int GetPages(this int elements, int pageSize)
        {
            return (int)Math.Ceiling((double)elements / pageSize);
        }
    }
}
