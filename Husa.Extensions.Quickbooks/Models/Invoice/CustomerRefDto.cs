namespace Husa.Extensions.Quickbooks.Models.Invoice
{
    public class CustomerRefDto
    {
        public CustomerRefDto(string value)
        {
            this.Value = value;
        }

        public string Value { get; set; }
    }
}
