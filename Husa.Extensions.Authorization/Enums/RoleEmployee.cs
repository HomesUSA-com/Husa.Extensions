namespace Husa.Extensions.Authorization.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum RoleEmployee
    {
        All = 0, // Role type All is not a valid option, just for UI request support
        [Display(Name = "Company Admin")]
        CompanyAdmin,
        [Display(Name = "Sales Employee")]
        SalesEmployee,
        [Display(Name = "No Role")]
        NoRole,
        [Display(Name = "Read-only")]
        Readonly,
        [Display(Name = "Sales Employee Read-only")]
        SalesEmployeeReadonly,
        [Display(Name = "Company Admin Read-only")]
        CompanyAdminReadonly,
    }
}
