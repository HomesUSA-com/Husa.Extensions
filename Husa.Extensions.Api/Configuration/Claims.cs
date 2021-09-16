namespace Husa.Extensions.Api.Configuration
{
    using System.Collections.Generic;

    public class Claims
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public IEnumerable<string> AllowedValues { get; set; }
    }
}
