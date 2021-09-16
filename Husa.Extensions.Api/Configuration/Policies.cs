namespace Husa.Extensions.Api.Configuration
{
    using System.Collections.Generic;

    public class Policies
    {
        public IEnumerable<Claims> Claims { get; set; }

        public IEnumerable<Roles> Roles { get; set; }
    }
}
