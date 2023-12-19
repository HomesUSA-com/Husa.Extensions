namespace Husa.Extensions.Quickbooks.Models.Invoice.InvoiceResponses
{
    using Newtonsoft.Json;

    public class InvoiceObjectResponse
    {
        public int Status { get; set; }

        public object Data { get; set; }

        public InvoiceDataDto ParseDataToInvoice()
        {
            string jsonData = this.Data.ToString();
            return JsonConvert.DeserializeObject<InvoiceDataDto>(jsonData);
        }
    }
}
