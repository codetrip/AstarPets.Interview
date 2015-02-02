using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AstarPets.Interview.Business.Basket;

namespace AstarPets.Interview.Business.Shipping
{
    [KnownType("KnownTypes")]
    public abstract class ShippingBase
    {
        public static IEnumerable<Type> KnownTypes()
        {
            return new[] { typeof(FlatRateShipping), typeof(PerRegionShipping), typeof(FarRegionShipping) };
        }
        
        public abstract string GetDescription(LineItem lineItem, Basket.Basket basket);

        public abstract decimal GetAmount(LineItem lineItem, Basket.Basket basket);
    }
}