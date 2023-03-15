namespace Husa.Extensions.Authorization
{
    using System;
    using Husa.Extensions.Authorization.Enums;
    using Husa.Extensions.Common.Enums;

    public interface IUserContext
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Email { get; set; }

        bool IsMLSAdministrator { get; set; }

        MarketCode? Market { get; set; }

        Guid? CompanyId { get; set; }

        UserRole UserRole { get; set; }

        RoleEmployee? EmployeeRole { get; set; }
    }
}
