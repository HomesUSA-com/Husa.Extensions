namespace Husa.Extensions.Quickbooks.Models.Invoice
{
    public class ClassRefDto
    {
        public ClassRefDto(string value)
        {
            this.Value = value;
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
