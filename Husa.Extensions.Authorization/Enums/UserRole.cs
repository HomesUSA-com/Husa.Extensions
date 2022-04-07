namespace Husa.Extensions.Authorization.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum UserRole
    {
        [Display(Name = "MLS Administrator")]
        MLSAdministrator,
        [Display(Name = "Photographer")]
        Photographer,
        [Display(Name = "User")]
        User,
    }
}
