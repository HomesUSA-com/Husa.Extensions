namespace Husa.Extensions.Api.Configuration
{
    using System.Collections.Generic;

    public class Policies
    {
        public const string CompanyApi = "CompanyAPIPolicy";

        public IEnumerable<Claims> Claims { get; set; }

        public IEnumerable<Roles> Roles { get; set; }
    }
}
