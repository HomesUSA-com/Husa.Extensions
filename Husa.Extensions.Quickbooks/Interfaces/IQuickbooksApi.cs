namespace Husa.Extensions.Quickbooks.Interfaces
{
    using System.Threading.Tasks;
    using Husa.Extensions.Quickbooks.Models.Invoice;
    using Husa.Extensions.Quickbooks.Models.Invoice.AttachResponse;
    using Husa.Extensions.Quickbooks.Models.Invoice.CustomerResponse;
    using Husa.Extensions.Quickbooks.Models.Invoice.InvoiceResponses;
    using Refit;

    public interface IQuickbooksApi
    {
        [Post("/invoice/create")]
        Task<InvoiceObjectResponse> PostDataAsync(CreateInvoiceDto invoiceData);

        [Get("/customer/get?customerId={customerId}")]
        Task<CustomerObjectResponse> GetCustomerRef([AliasAs("customerId")] int customerId);

        [Headers("Content-Type: application/json; charset=utf-8")]
        [Post("/Attachable/upload")]
        Task<AttachableObjectResponse> AttachPdfToInvoice(InvoiceAttachDto invoiceAttachDto);
    }
}
