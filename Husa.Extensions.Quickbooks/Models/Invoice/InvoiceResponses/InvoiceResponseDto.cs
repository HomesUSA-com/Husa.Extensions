namespace Husa.Extensions.Quickbooks.Models.Invoice.InvoiceResponses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class InvoiceResponseDto
    {
        [JsonProperty("txnDate")]
        public string EnteredDateWhenTransactionOccurred { get; set; }
        public string Domain { get; set; }
        public string PrintStatus { get; set; }
        public object SalesTermRef { get; set; }
        public decimal TotalAmt { get; set; }

        [JsonProperty("line")]
        public List<LineResponseDto> Line { get; set; }
        public string DueDate { get; set; }
        public bool ApplyTaxAfterDiscount { get; set; }
        public string DocNumber { get; set; }
        public bool Sparse { get; set; }
        public object CustomerMemo { get; set; }
        public decimal Deposit { get; set; }
        public decimal Balance { get; set; }
        public CustomerRefDto CustomerRef { get; set; }

        [JsonProperty("txnTaxDetail")]
        public object TaxChargedTransactionDetail { get; set; }
        public string SyncToken { get; set; }

        [JsonProperty("linkedTxn")]
        public List<object> RelatedTransactionsToInvoice { get; set; }
        public object BillEmail { get; set; }
        public AddressDto ShipAddr { get; set; }
        public string EmailStatus { get; set; }
        public AddressDto BillAddr { get; set; }
        public MetaDataDto MetaData { get; set; }
        public List<object> CustomField { get; set; }
        public string Id { get; set; }
    }
}
