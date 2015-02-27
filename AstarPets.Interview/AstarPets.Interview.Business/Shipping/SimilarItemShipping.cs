using System;
using System.Collections.Generic;
using System.Linq;
using AstarPets.Interview.Business.Basket;

namespace AstarPets.Interview.Business.Shipping
{
    public class SimilarItemShipping : PerRegionShipping
    {
        public decimal DiscountAmount { get; set; }
        public IEnumerable<string> SimilarProperties { get; set; }

        public override decimal GetAmount(LineItem lineItem, Basket.Basket basket)
        {
            // Return base amount - discount value but don't go below 0
            return Math.Max(base.GetAmount(lineItem, basket) - GetDiscount(lineItem, basket), 0.0m);
        }

        private decimal GetDiscount(LineItem lineItem, Basket.Basket basket)
        {
            // Check the values of all of the specified similar properties.
            // If all are equal to another basket item, return discount amount. Else 0
            foreach (var item in basket.LineItems.Where(li => !li.Equals(lineItem)))
            {
                var matches = true;
                foreach (var property in SimilarProperties)
                {
                    var thisVal = GetPropertyValue(lineItem, property);
                    var thatVal = GetPropertyValue(item, property);
                    matches = matches && thisVal.Equals(thatVal);
                }

                if (matches) return DiscountAmount;
            }
            return 0.0m;
        }

        // Get property value of line item using reflection. This is comparitively slow but allows the
        // properties that enable discount to be configurable
        private static object GetPropertyValue(LineItem lineItem, string propertyName)
        {
            var property = lineItem.GetType().GetProperty(propertyName);

            if (property == null) return null;

            var value = property.GetValue(lineItem, null);
            //Special case for shipping, just ensure that they are the same type (the instances don't have to be equal)
            return propertyName == "Shipping" ? value.GetType().Name : value;
        }
    }
}
