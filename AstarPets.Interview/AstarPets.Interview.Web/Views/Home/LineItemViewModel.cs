namespace AstarPets.Interview.Web.Views.Home
{
    public class LineItemViewModel
    {
        public string ProductId { get; set; }
        public decimal Amount { get; set; }
        public int SupplierId { get; set; }
        public string ShippingOption { get; set; }
        public string DeliveryRegion { get; set; }
    }
}