namespace Husa.Extensions.Quickbooks.Models.Invoice
{
    public class ItemRefDto
    {
        public ItemRefDto(string value)
        {
            this.Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
