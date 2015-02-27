using System.Collections.Generic;
using System.Linq;
using AstarPets.Interview.Business.Basket;

namespace AstarPets.Interview.Business.Shipping
{
    public class NewShipping : ShippingBase
    {
        private const decimal _duplicateDiscount = 0.5m;

        public IEnumerable<RegionShippingCost> PerRegionCosts { get; set; }

        public override string GetDescription(LineItem lineItem, Basket.Basket basket)
        {
            return string.Format("Shipping to {0}", lineItem.DeliveryRegion);
        }

        public override decimal GetAmount(LineItem lineItem, Basket.Basket basket)
        {
            bool applyDiscount = basket.LineItems.Any (i => i.DeliveryRegion == lineItem.DeliveryRegion
                                                         && i.SupplierId == lineItem.SupplierId
                                                         && i.Shipping == lineItem.Shipping);
            return
                (from c in PerRegionCosts
                 where c.DestinationRegion == lineItem.DeliveryRegion
                 select applyDiscount ? c.Amount - _duplicateDiscount : c.Amount).Single();
        }
    }
}