namespace Husa.Extensions.Api.Configuration
{
    using System.Collections.Generic;

    public class Roles
    {
        public const string MLSAdministrator = "MLSAdministrator";
        public const string Photographer = "Photographer";
        public const string User = "User";

        public string Name { get; set; }

        public IEnumerable<string> RolesClaim { get; set; }
    }
}
