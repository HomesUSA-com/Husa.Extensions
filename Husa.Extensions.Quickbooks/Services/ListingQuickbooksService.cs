namespace Husa.Extensions.Quickbooks.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using DinkToPdf;
    using DinkToPdf.Contracts;
    using Husa.Extensions.Common.Enums;
    using Husa.Extensions.Common.Exceptions;
    using Husa.Extensions.Quickbooks.Interfaces;
    using Husa.Extensions.Quickbooks.Models.Invoice;
    using Husa.Extensions.Quickbooks.Models.Invoice.InvoiceResponses;
    using Microsoft.Extensions.Logging;

    public class ListingQuickbooksService : IListingQuickbooksService
    {
        public const string SalesItemLineId = "SalesItemLineDetail";
        public const string ClassRefId = "SalesItemLineDetail";
        public const string InvoiceDetailType = "SalesItemLineDetail";
        public const string EntityTypeInvoice = "Invoice";
        public const string ReListedName = "Relisted";
        public const string ComparableName = "Comparable";
        public const string NewListingName = "New Listing";
        public const string ActiveTransferName = "Active Transfer";
        public const string PendingTransferName = "Pending Transfer";

        private readonly IQuickbooksApi quickbooksApi;
        private readonly IConverter converter;
        private readonly ILogger<ListingQuickbooksService> logger;

        public ListingQuickbooksService(
            IConverter converter,
            IQuickbooksApi quickbooksApi,
            ILogger<ListingQuickbooksService> logger)
        {
            this.converter = converter ?? throw new ArgumentNullException(nameof(converter));
            this.quickbooksApi = quickbooksApi ?? throw new ArgumentNullException(nameof(quickbooksApi));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<InvoiceDataDto> CreateInvoice(IEnumerable<LineDto> listLine, string customerRef, IEnumerable<BillingListingDto> billListing, Dictionary<string, double> prices, string companyName)
        {
            this.logger.LogInformation("Creating invoice for customerRef: {customerRef}", customerRef);
            var customerReference = new CustomerRefDto(customerRef);
            var createInvoice = new CreateInvoiceDto(listLine, customerReference);

            var result = await this.quickbooksApi.PostDataAsync(createInvoice);

            if (result.Status != (int)HttpStatusCode.OK)
            {
                throw new DomainException("Error creating invoice");
            }

            var response = result.ParseDataToInvoice();

            await this.GetPdf(billListing, prices, response.Invoice.Id, companyName);

            return response;
        }

        public async Task<bool> CustomerRefExist(int? customerRef)
        {
            if (customerRef is null || customerRef <= 0)
            {
                this.logger.LogInformation("Customer ref with id: {customerRef} is not valid, are inactive or request was failed", customerRef);
                return false;
            }

            this.logger.LogInformation("Getting customer ref");
            var result = await this.quickbooksApi.GetCustomerRef((int)customerRef);
            if (result.Status != (int)HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }

        private static string MapActionName(string actionName)
        {
            switch (actionName.ToLower())
            {
                case "newlisting":
                    return NewListingName;
                case "comparable":
                    return ComparableName;
                case "relist":
                    return ReListedName;
                case "activetransfer":
                    return ActiveTransferName;
                case "pendingtransfer":
                    return PendingTransferName;
                default:
                    throw new ArgumentException("Unknown action name", nameof(actionName));
            }
        }

        private async Task GetPdf(IEnumerable<BillingListingDto> billListing, Dictionary<string, double> prices, string invoiceId, string companyName)
        {
            this.logger.LogInformation("Building PDF file");
            var groupedListing = billListing
                .GroupBy(x => x.PublishType)
                .ToDictionary(r => r.Key, r => r.ToList());

            double fullTotal = 0;
            StringBuilder htmlContent = new StringBuilder();

            htmlContent.AppendLine("<html><head>");
            htmlContent.AppendLine("<style type='text/css'>");
            htmlContent.AppendLine(".myTable { background-color:#eee;border-collapse:collapse;width:100%;font-family:Arial; }");
            htmlContent.AppendLine(".myTable th { background-color:#003366;color:white; }");
            htmlContent.AppendLine(".myTable td, .myTable th { padding:5px;border:1px solid #000;font-size:20px }");
            htmlContent.AppendLine(".logo-container { background-color:#003366;text-align:center;display:table;width:100%;padding: 5px 0;margin:0 auto;");
            htmlContent.AppendLine("</style>");
            htmlContent.AppendLine("</head><body>");
            htmlContent.AppendLine("<html><body>");
            htmlContent.AppendLine("<br />");

            foreach (var pair in groupedListing)
            {
                var rowNumber = 1;
                htmlContent.AppendLine("<div class='logo-container'><img src='https://homesusastorage.blob.core.windows.net/webimages/SpecDeck-White-Logo-small.png' alt='Description' /></div>");
                htmlContent.AppendLine("<table class='myTable'>");
                htmlContent.AppendLine("<thead>");
                htmlContent.AppendLine("<tr>");
                htmlContent.AppendLine("<th>ROW</th>");
                htmlContent.AppendLine("<th>MLS #</th>");
                htmlContent.AppendLine("<th>STAGE</th>");
                htmlContent.AppendLine("<th>LIST DATE</th>");
                htmlContent.AppendLine("<th>SUBDIVISION</th>");
                htmlContent.AppendLine("<th>ADDRESS</th>");
                htmlContent.AppendLine("<th>FEE DESC.</th>");
                htmlContent.AppendLine("<th>LIST FEE</th>");
                htmlContent.AppendLine("</tr>");
                htmlContent.AppendLine("</thead>");
                htmlContent.AppendLine("<tbody>");

                foreach (var listing in pair.Value)
                {
                    htmlContent.AppendLine("<tr>");
                    htmlContent.AppendLine($"<td>{rowNumber++}</td>");
                    htmlContent.AppendLine($"<td>{listing.MlsNumber}</td>");
                    htmlContent.AppendLine($"<td>{listing.MarketStatus}</td>");
                    htmlContent.AppendLine($"<td>{listing.ListDate.Value.Date.ToString("MM/dd/yyyy")}</td>");
                    htmlContent.AppendLine($"<td>{listing.Subdivision}</td>");
                    htmlContent.AppendLine($"<td>{listing.StreetNum + " " + listing.StreetName}</td>");
                    htmlContent.AppendLine($"<td>{MapActionName(listing.PublishType.ToString())}</td>");
                    htmlContent.AppendLine($"<td>{string.Format("${0:#,0}", prices[listing.PublishType])}</td>");
                    htmlContent.AppendLine("</tr>");
                }

                var publishTypeTotal = prices[pair.Key] * pair.Value.Count;
                fullTotal += publishTypeTotal;
                htmlContent.AppendLine("</tbody>");
                htmlContent.AppendLine("<tfoot>");
                htmlContent.AppendLine($"<tr><th colspan='7' style='text-align: right;background-color:#eee;color:#000;'>Total {MapActionName(pair.Key.ToString())}:</th><td>{string.Format("${0:#,0}", publishTypeTotal)}</td></tr>");
                htmlContent.AppendLine("</tfoot>");
                htmlContent.AppendLine("</table>");
                htmlContent.AppendLine("<br />");
            }

            htmlContent.AppendLine($"<div><P align='right' style='font-size:20px;font-family:Arial;font-weight:bold;'>Total: {string.Format("${0:#,0}", fullTotal)}</p></div>");
            htmlContent.AppendLine("</body></html>");

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        HtmlContent = htmlContent.ToString(),
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings =
                        {
                            FontName = "arial",
                            FontSize = 12,
                            Right = $"Invoice generated for {companyName} at {DateTime.UtcNow.Date.ToString("MM/dd/yyyy")}",
                            Line = true,
                        },
                    },
                },
            };

            byte[] pdfData = this.converter.Convert(doc);
            var fileString = Convert.ToBase64String(pdfData);
            var pdfName = companyName + "_" + MarketCode.SanAntonio + "_" + DateTime.Now.ToString("MM/dd/yyyy").Replace('/', '-') + ".pdf";
            var invoiceAttach = new InvoiceAttachDto(invoiceId, pdfName, EntityTypeInvoice, "application/pdf", fileString);
            await this.AttachePDFToInvoiceAsync(invoiceAttach);
        }

        private async Task AttachePDFToInvoiceAsync(InvoiceAttachDto invoiceAttachDto)
        {
            if (invoiceAttachDto == null)
            {
                throw new DomainException($"Invoice attach object cannot be null");
            }

            this.logger.LogInformation("Attaching PDF file to existing invoice with id: {invoiceId}", invoiceAttachDto.InvoiceId);
            var result = await this.quickbooksApi.AttachPdfToInvoice(invoiceAttachDto);
            if (result.Status != (int)HttpStatusCode.OK)
            {
                throw new DomainException("Error when trying attach pdf to invoice");
            }
        }
    }
}
