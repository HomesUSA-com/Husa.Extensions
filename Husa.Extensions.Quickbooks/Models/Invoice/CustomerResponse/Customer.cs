namespace Husa.Extensions.Quickbooks.Models.Invoice.CustomerResponse
{
    public class Customer
    {
        public PrimaryEmailAddr PrimaryEmailAddr { get; set; }

        public string SyncToken { get; set; }

        public string Domain { get; set; }

        public string GivenName { get; set; }

        public string DisplayName { get; set; }

        public bool BillWithParent { get; set; }

        public string FullyQualifiedName { get; set; }

        public string CompanyName { get; set; }

        public string FamilyName { get; set; }

        public bool Sparse { get; set; }

        public PrimaryPhone PrimaryPhone { get; set; }

        public bool Active { get; set; }

        public bool Job { get; set; }

        public double BalanceWithJobs { get; set; }

        public BillAddr BillAddr { get; set; }

        public string PreferredDeliveryMethod { get; set; }

        public bool Taxable { get; set; }

        public string PrintOnCheckName { get; set; }

        public double Balance { get; set; }

        public string Id { get; set; }

        public MetaDataCustomer MetaData { get; set; }
    }
}
