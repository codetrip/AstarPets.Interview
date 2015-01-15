using System.Collections.Generic;
using AstarPets.Interview.Business.Basket;
using AstarPets.Interview.Business.Shipping;
using NUnit.Framework;

namespace AstarPets.Interview.Tests
{
    [TestFixture]
    class PerRegionMultiDiscountShippingTests
    {
        [Test]
        public void PerRegionMultiDiscountShippingOptionTest_NoDiscount_DifferentRegion()
        {
            var lineItem1 = new LineItem() { DeliveryRegion = RegionShippingCost.Regions.Europe, SupplierId = 23};
            var lineItem2 = new LineItem() { DeliveryRegion = RegionShippingCost.Regions.UK, SupplierId = 23 };

            var basket = new Basket
            {
                LineItems = new List<LineItem> { lineItem1, lineItem2 }
            };

            var perRegionMultiDiscountShippingOption = SetUpShipping();

            var shippingAmount = perRegionMultiDiscountShippingOption.GetAmount(lineItem1, basket);
            Assert.That(shippingAmount, Is.EqualTo(1.5m), "item1 expected shipping amount");


            shippingAmount = perRegionMultiDiscountShippingOption.GetAmount(lineItem2, basket);
            Assert.That(shippingAmount, Is.EqualTo(.75m), "item2 expected shipping amount");
        }

        [Test]
        public void PerRegionMultiDiscountShippingOptionTest_NoDiscount_DifferentSupplier()
        {
            var lineItem1 = new LineItem() { DeliveryRegion = RegionShippingCost.Regions.Europe, SupplierId = 23 };
            var lineItem2 = new LineItem() { DeliveryRegion = RegionShippingCost.Regions.Europe, SupplierId = 24 };

            var basket = new Basket
            {
                LineItems = new List<LineItem> { lineItem1, lineItem2 }
            };

            var perRegionMultiDiscountShippingOption = SetUpShipping();

            var shippingAmount = perRegionMultiDiscountShippingOption.GetAmount(lineItem1, basket);
            Assert.That(shippingAmount, Is.EqualTo(1.5m), "item1 expected shipping amount");


            shippingAmount = perRegionMultiDiscountShippingOption.GetAmount(lineItem2, basket);
            Assert.That(shippingAmount, Is.EqualTo(1.5m), "item2 expected shipping amount");
        }

        [Test]
        public void PerRegionMultiDiscountShippingOptionTest_DiscountApplied()
        {
            var lineItem1 = new LineItem() { DeliveryRegion = RegionShippingCost.Regions.Europe, SupplierId = 23 };
            var lineItem2 = new LineItem() { DeliveryRegion = RegionShippingCost.Regions.Europe, SupplierId = 23 };

            var basket = new Basket
            {
                LineItems = new List<LineItem> { lineItem1, lineItem2 }
            };

            var perRegionMultiDiscountShippingOption = SetUpShipping();

            var shippingAmount = perRegionMultiDiscountShippingOption.GetAmount(lineItem1, basket);
            Assert.That(shippingAmount, Is.EqualTo(1.0m), "item1 expected shipping amount");


            shippingAmount = perRegionMultiDiscountShippingOption.GetAmount(lineItem2, basket);
            Assert.That(shippingAmount, Is.EqualTo(1.0m), "item2 expected shipping amount");
        }

        private static PerRegionWithMultiItemDiscountShipping SetUpShipping()
        {
            var perRegionMultiDiscountShippingOption = new PerRegionWithMultiItemDiscountShipping()
            {
                PerRegionCosts = new[]
                {
                    new RegionShippingCost()
                    {
                        DestinationRegion =
                            RegionShippingCost.Regions.UK,
                        Amount = .75m
                    },
                    new RegionShippingCost()
                    {
                        DestinationRegion =
                            RegionShippingCost.Regions.Europe,
                        Amount = 1.5m
                    }
                },
                Discount = 0.5m
            };
            return perRegionMultiDiscountShippingOption;
        }
    }
}
