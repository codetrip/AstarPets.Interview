namespace AstarPets.Interview.Business.Shipping
{
    public class RegionShippingCost
    {
        public string DestinationRegion { get; set; }
        public decimal Amount { get; set; }

        public static class Regions
        {
            public const string UK = "UK";
            public const string Europe = "Europe";
            public const string RestOfTheWorld = "RestOfTheWorld";
        }
    }
}