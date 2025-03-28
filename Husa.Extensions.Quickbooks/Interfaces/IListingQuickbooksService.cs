namespace Husa.Extensions.Quickbooks.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Husa.Extensions.Quickbooks.Models.Invoice;
    using Husa.Extensions.Quickbooks.Models.Invoice.InvoiceResponses;

    public interface IListingQuickbooksService
    {
        Task<InvoiceDataDto> CreateInvoice(IEnumerable<LineDto> listLine, string customerRef, IEnumerable<BillingListingDto> billListing, Dictionary<string, double> prices, string companyName);
        Task<InvoiceDataDto> CreatePhotoRequestInvoice(IEnumerable<LineDto> listLine, string customerRef, IEnumerable<BillingPhotoRequestDto> billListing, Dictionary<string, double> prices, string companyName);
        Task<bool> CustomerRefExist(int? customerRef);
    }
}
