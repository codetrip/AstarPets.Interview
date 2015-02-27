using System.Collections.Generic;
using NUnit.Framework;
using AstarPets.Interview.Business.Basket;
using AstarPets.Interview.Business.Shipping;

namespace AstarPets.Interview.Tests
{
    [TestFixture]
    public class ShippingOptionTests
    {
        [Test]
        public void FlatRateShippingOptionTest()
        {
            var flatRateShippingOption = new FlatRateShipping {FlatRate = 1.5m};
            var shippingAmount = flatRateShippingOption.GetAmount(new LineItem(), new Basket());

            Assert.That(shippingAmount, Is.EqualTo(1.5m), "Flat rate shipping not correct.");
        }

        [Test]
        public void PerRegionShippingOptionTest()
        {
            var perRegionShippingOption = new PerRegionShipping()
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
                                              };

            var shippingAmount = perRegionShippingOption.GetAmount(new LineItem() {DeliveryRegion = RegionShippingCost.Regions.Europe}, new Basket());
            Assert.That(shippingAmount, Is.EqualTo(1.5m));

            shippingAmount = perRegionShippingOption.GetAmount(new LineItem() { DeliveryRegion = RegionShippingCost.Regions.UK}, new Basket());
            Assert.That(shippingAmount, Is.EqualTo(.75m));
        }

        [Test]
        public void SimilarItemAppliesDiscountTest()
        {
            var similarItemShippingOption = new SimilarItemShipping
            {
                DiscountAmount = 0.5m,
                SimilarProperties = new[] {"SupplierId", "DeliveryRegion", "Shipping"},
                PerRegionCosts = new[]
                {
                    new RegionShippingCost {Amount = 0.5m, DestinationRegion = RegionShippingCost.Regions.UK},
                    new RegionShippingCost {Amount = 1m, DestinationRegion = RegionShippingCost.Regions.Europe},
                    new RegionShippingCost {Amount = 2m, DestinationRegion = RegionShippingCost.Regions.RestOfTheWorld}
                }
            };

            var basket = new Basket
            {
                LineItems =
                    new List<LineItem>
                    {
                        new LineItem
                        {
                            Amount = 2m,
                            SupplierId = 1,
                            DeliveryRegion = RegionShippingCost.Regions.RestOfTheWorld,
                            Shipping = similarItemShippingOption
                        },
                        new LineItem
                        {
                            Amount = 5m,
                            SupplierId = 1,
                            DeliveryRegion = RegionShippingCost.Regions.RestOfTheWorld,
                            Shipping = similarItemShippingOption
                        }
                    }
            };

            var calculator = new ShippingCalculator();
            var basketShipping = calculator.CalculateShipping(basket);

            Assert.That(basketShipping, Is.EqualTo(3m));
        }

        [Test]
        public void NoDiscountForNotSameShippingTest()
        {
            var similarItemShippingOption = new SimilarItemShipping
            {
                DiscountAmount = 0.5m,
                SimilarProperties = new[] { "SupplierId", "DeliveryRegion", "Shipping" },
                PerRegionCosts = new[]
                {
                    new RegionShippingCost {Amount = 0.5m, DestinationRegion = RegionShippingCost.Regions.UK},
                    new RegionShippingCost {Amount = 1m, DestinationRegion = RegionShippingCost.Regions.Europe},
                    new RegionShippingCost {Amount = 2m, DestinationRegion = RegionShippingCost.Regions.RestOfTheWorld}
                }
            };

            var flatRateShippingOption = new FlatRateShipping { FlatRate = 1m };

            var basket = new Basket
            {
                LineItems =
                    new List<LineItem>
                    {
                        new LineItem
                        {
                            Amount = 2m,
                            SupplierId = 1,
                            DeliveryRegion = RegionShippingCost.Regions.RestOfTheWorld,
                            Shipping = similarItemShippingOption
                        },
                        new LineItem
                        {
                            Amount = 5m,
                            SupplierId = 1,
                            DeliveryRegion = RegionShippingCost.Regions.RestOfTheWorld,
                            Shipping = flatRateShippingOption
                        }
                    }
            };

            var calculator = new ShippingCalculator();
            var basketShipping = calculator.CalculateShipping(basket);

            Assert.That(basketShipping, Is.EqualTo(3m));
        }
        
        [Test]
        public void NoDiscountForNotSameProperties()
        {
            var similarItemShippingOption = new SimilarItemShipping
            {
                DiscountAmount = 0.5m,
                SimilarProperties = new[] { "SupplierId", "DeliveryRegion" },
                PerRegionCosts = new[]
                {
                    new RegionShippingCost {Amount = 0.5m, DestinationRegion = RegionShippingCost.Regions.UK},
                    new RegionShippingCost {Amount = 1m, DestinationRegion = RegionShippingCost.Regions.Europe},
                    new RegionShippingCost {Amount = 2m, DestinationRegion = RegionShippingCost.Regions.RestOfTheWorld}
                }
            };

            var basket = new Basket
            {
                LineItems =
                    new List<LineItem>
                    {
                        new LineItem
                        {
                            Amount = 2m,
                            SupplierId = 2,
                            DeliveryRegion = RegionShippingCost.Regions.Europe,
                            Shipping = similarItemShippingOption
                        },
                        new LineItem
                        {
                            Amount = 5m,
                            SupplierId = 1,
                            DeliveryRegion = RegionShippingCost.Regions.RestOfTheWorld,
                            Shipping = similarItemShippingOption
                        }
                    }
            };

            var calculator = new ShippingCalculator();
            var basketShipping = calculator.CalculateShipping(basket);

            Assert.That(basketShipping, Is.EqualTo(3m));
        }

        [Test]
        public void BasketShippingTotalTest()
        {
            var perRegionShippingOption = new PerRegionShipping()
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
            };

            var flatRateShippingOption = new FlatRateShipping {FlatRate = 1.1m};

            var basket = new Basket()
                             {
                                 LineItems = new List<LineItem>
                                                 {
                                                     new LineItem()
                                                         {
                                                             DeliveryRegion = RegionShippingCost.Regions.UK,
                                                             Shipping = perRegionShippingOption
                                                         },
                                                     new LineItem()
                                                         {
                                                             DeliveryRegion = RegionShippingCost.Regions.Europe,
                                                             Shipping = perRegionShippingOption
                                                         },
                                                     new LineItem() {Shipping = flatRateShippingOption},
                                                 }
                             };

            var calculator = new ShippingCalculator();

            decimal basketShipping = calculator.CalculateShipping(basket);

            Assert.That(basketShipping, Is.EqualTo(3.35m));
        }
    }
}