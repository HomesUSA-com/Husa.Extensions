namespace Husa.Extensions.Authorization.Models
{
    using System;
    using Husa.Extensions.Authorization.Enums;

    public class UserContext : IUserContext
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsMLSAdministrator { get; set; }

        public Guid? CompanyId { get; set; }

        public UserRole UserRole { get; set; }

        public RoleEmployee? EmployeeRole { get; set; }
    }
}
