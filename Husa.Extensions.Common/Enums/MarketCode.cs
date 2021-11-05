namespace Husa.Extensions.Common.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum MarketCode
    {
        [Display(Name = "DFW")]
        DFW,
        [Display(Name = "Austin")]
        Austin,
        [Display(Name = "San Antonio")]
        SanAntonio,
        [Display(Name = "Houston")]
        Houston,
        [Display(Name = "Longview")]
        Longview,
        [Display(Name = "Tyler")]
        Tyler,
        [Display(Name = "Waco")]
        Waco,
    }
}
