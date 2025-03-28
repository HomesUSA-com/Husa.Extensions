namespace Husa.Extensions.Quickbooks.Models.Invoice
{
    using System;

    public class ItemDetailPhotoDto
    {
        public ItemDetailPhotoDto(string itemRefId, string classRefId)
        {
            this.StillsHome = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.StillsExteriorOnly = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.Twilight = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.VirtualTour = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.VirtualStagingOneImage = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.VirtualStagingThreeImage = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.VirtualStagingSixImage = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.CommunityOptionOne = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.CommunityOptionTwo = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.CommunityOptionThree = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.TripCharge = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.PhotoshopEditing = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.StillsHome.TotalAmount =
            this.StillsExteriorOnly.TotalAmount =
            this.Twilight.TotalAmount =
            this.VirtualTour.TotalAmount =
            this.VirtualStagingOneImage.TotalAmount =
            this.VirtualStagingThreeImage.TotalAmount =
            this.VirtualStagingSixImage.TotalAmount =
            this.CommunityOptionOne.TotalAmount =
            this.CommunityOptionTwo.TotalAmount =
            this.CommunityOptionThree.TotalAmount =
            this.TripCharge.TotalAmount =
            this.PhotoshopEditing.TotalAmount = 0;
            this.TotalInvoice = 0;
        }

        public SalesItemLineDetailDto StillsHome { get; set; }
        public SalesItemLineDetailDto StillsExteriorOnly { get; set; }
        public SalesItemLineDetailDto Twilight { get; set; }
        public SalesItemLineDetailDto VirtualTour { get; set; }
        public SalesItemLineDetailDto VirtualStagingOneImage { get; set; }
        public SalesItemLineDetailDto VirtualStagingThreeImage { get; set; }
        public SalesItemLineDetailDto VirtualStagingSixImage { get; set; }
        public SalesItemLineDetailDto CommunityOptionOne { get; set; }
        public SalesItemLineDetailDto CommunityOptionTwo { get; set; }
        public SalesItemLineDetailDto CommunityOptionThree { get; set; }
        public SalesItemLineDetailDto TripCharge { get; set; }
        public SalesItemLineDetailDto PhotoshopEditing { get; set; }
        public double TotalInvoice { get; set; }
        public string CustomerRef { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InvoiceType { get; set; }
    }
}
