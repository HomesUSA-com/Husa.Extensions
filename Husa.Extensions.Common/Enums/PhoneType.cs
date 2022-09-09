namespace Husa.Extensions.Common.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum PhoneType
    {
        [Display(Name = "Business Phone")]
        Business,
        [Display(Name = "Mobile Phone")]
        Mobile,
        [Display(Name = "Other Phone")]
        Other,
        [Display(Name = "Fax")]
        Fax,
        [Display(Name = "Alt Phone")]
        Alternative,
        [Display(Name = "Appt Phone")]
        Appointment,
    }
}
