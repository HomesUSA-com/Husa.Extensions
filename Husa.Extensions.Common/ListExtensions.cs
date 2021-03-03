namespace Husa.Extensions.Common
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ListExtensions
    {
        public static IEnumerable<int> ToInt(this List<string> elements)
        {
            return elements.Select(intElement =>
                {
                    _ = int.TryParse(intElement, out var intValue);
                    return intValue;
                }).ToList();
        }
    }
}
