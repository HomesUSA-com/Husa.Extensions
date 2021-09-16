namespace Husa.Extensions.Api.Configuration
{
    using System.Collections.Generic;

    public class Roles
    {
        public string Name { get; set; }

        public IEnumerable<string> RolesClaim { get; set; }
    }
}
