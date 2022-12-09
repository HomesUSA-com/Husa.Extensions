namespace Husa.Extensions.Common.Enums
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum MarketCode
    {
        [Display(Name = "DFW")]
        [Description("DFW")]
        DFW,
        [Display(Name = "Austin")]
        [Description("Austin")]
        Austin,
        [Display(Name = "San Antonio")]
        [Description("SanAntonio")]
        SanAntonio,
        [Display(Name = "Houston")]
        [Description("Houston")]
        Houston,
        [Display(Name = "Longview")]
        [Description("Longview")]
        Longview,
        [Display(Name = "Tyler")]
        [Description("Tyler")]
        Tyler,
        [Display(Name = "Waco")]
        [Description("Waco")]
        Waco,
    }
}
