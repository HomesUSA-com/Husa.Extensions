namespace Husa.Extensions.Common.Classes
{
    using System.Collections.Generic;

    public class DataSet<T>
        where T : class
    {
        public DataSet(IEnumerable<T> data, int total)
        {
            this.Data = data;
            this.Total = total;
        }

        public IEnumerable<T> Data { get; set; }
        public int Total { get; set; }
    }
}
