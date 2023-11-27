namespace Husa.Extensions.Quickbooks.Models.Invoice.InvoiceResponses
{
    using Newtonsoft.Json;

    public class InvoiceDataDto
    {
        [JsonProperty("invoice")]
        public InvoiceResponseDto Invoice { get; set; }
    }
}
