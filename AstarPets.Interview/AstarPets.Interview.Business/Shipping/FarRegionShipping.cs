using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using AstarPets.Interview.Business.Basket;

namespace AstarPets.Interview.Business.Shipping
{
    public class FarRegionShipping : ShippingBase
    {
        public IEnumerable<RegionShippingCost> FarRegionCosts { get; set; }

        public decimal SpecialDeduction
        {
            get { return Convert.ToDecimal(ConfigurationManager.AppSettings["FarRegionShippingDeduction"]); }
        }

        public override string GetDescription(LineItem lineItem, Basket.Basket basket)
        {
            return string.Format("Shipping to {0}", lineItem.DeliveryRegion);
        }

        public override decimal GetAmount(LineItem lineItem, Basket.Basket basket)
        {
            return (HasSpecialAmount(lineItem, basket)) ? SpecialDeduction :
                (from c in FarRegionCosts
                 where c.DestinationRegion.Equals(lineItem.DeliveryRegion)
                 select c.Amount).Single();
        }

        private bool HasSpecialAmount(LineItem lineItem,Basket.Basket basket)
        {
            return  (basket.LineItems != null &&  basket.LineItems.Count(item => item.DeliveryRegion.Equals(lineItem.DeliveryRegion) &&
                                                  item.SupplierId == lineItem.SupplierId &&
                                                  item.Shipping is FarRegionShipping) > 1);
        }
    }
}
