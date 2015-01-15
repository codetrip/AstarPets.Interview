using System;
using System.Collections.Generic;
using System.Linq;
using AstarPets.Interview.Business.Basket;

namespace AstarPets.Interview.Business.Shipping
{
    public class PerRegionWithMultiItemDiscountShipping : ShippingBase
    {
        public IEnumerable<RegionShippingCost> PerRegionCosts { get; set; }
        public decimal Discount { get; set; }

        public override string GetDescription(LineItem lineItem, Basket.Basket basket)
        {
            return string.Format("Shipping to {0}", lineItem.DeliveryRegion);
        }

        public override decimal GetAmount(LineItem lineItem, Basket.Basket basket)
        {
            // per region cost 
            var cost = 
                (from c in PerRegionCosts
                 where c.DestinationRegion == lineItem.DeliveryRegion
                 select c.Amount).Single();

            var discountedCost = ApplyDiscount(lineItem, basket, cost);

            return discountedCost;
        }

        private decimal ApplyDiscount(LineItem lineItem, Basket.Basket basket, decimal cost)
        {
            if (basket.LineItems.Count(
                l => l.DeliveryRegion == lineItem.DeliveryRegion && l.SupplierId == lineItem.SupplierId) > 1)
            {
                return cost - Discount;
            }

            return cost;
        }
    }
}
