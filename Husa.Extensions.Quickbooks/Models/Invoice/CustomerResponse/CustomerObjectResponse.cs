namespace Husa.Extensions.Quickbooks.Models.Invoice.CustomerResponse
{
    using Newtonsoft.Json;

    public class CustomerObjectResponse
    {
        public int Status { get; set; }

        public GetCustomer Data { get; set; }

        public GetCustomer ParseDataToCustomer()
        {
            string jsonData = this.Data.ToString();
            return JsonConvert.DeserializeObject<GetCustomer>(jsonData);
        }
    }
}
