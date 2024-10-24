namespace Husa.Extensions.Downloader.Trestle.Configuration
{
    using System.ComponentModel.DataAnnotations;

    public class MarketLogin
    {
        [Required(AllowEmptyStrings = false)]
        public string ClientId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string ClientSecret { get; set; }
    }
}
