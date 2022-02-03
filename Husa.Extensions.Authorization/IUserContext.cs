namespace Husa.Extensions.Authorization
{
    using System;
    using Husa.Extensions.Authorization.Enums;

    public interface IUserContext
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Email { get; set; }

        bool IsMLSAdministrator { get; set; }

        Guid? CompanyId { get; set; }

        RoleEmployee? EmployeeRole { get; set; }
    }
}
