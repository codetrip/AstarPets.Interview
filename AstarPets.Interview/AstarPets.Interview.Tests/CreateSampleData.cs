using System.Collections.Generic;
using System.IO;
using System.Linq;
using AstarPets.Interview.Web;
using AstarPets.Interview.Web.IoC;
using NUnit.Framework;
using AstarPets.Interview.Business.Core;
using AstarPets.Interview.Business.Shipping;

namespace AstarPets.Interview.Tests
{
    [TestFixture]
    public class CreateSampleData
    {
        [Test]
        public void CreateSampleShippingOptions()
        {
            var regionShippingCosts = new List<RegionShippingCost>
            {
                new RegionShippingCost{DestinationRegion = RegionShippingCost.Regions.UK, Amount = .5m},
                new RegionShippingCost{DestinationRegion = RegionShippingCost.Regions.Europe, Amount = 1m},
                new RegionShippingCost{DestinationRegion = RegionShippingCost.Regions.RestOfTheWorld, Amount = 2m},
            };

            var shippings = new Dictionary<string, ShippingBase>
                                {
                                    {"FlatRate", new FlatRateShipping{FlatRate = 1.5m}},
                                    {"PerRegion", new PerRegionShipping{PerRegionCosts = regionShippingCosts}},
                                    {"PerRegionWithMultiDiscount", new PerRegionWithMultiItemDiscountShipping{ Discount = 0.5m, PerRegionCosts = regionShippingCosts}},
                                };

            var ser = SerializationHelper.DataContractSerialize(shippings);

            using (var fileWriter = new StreamWriter(@"..\..\..\AstarPets.Interview.Web\App_Data\Shipping.xml", false))
            {
                fileWriter.Write(ser);
            }

        }

        [Test]
        public void RegistrationTest()
        {
            ObjectFactory.WindsorContainer.Install(new WindsorInstaller());
        }

        [Test]
        public void GetConstants()
        {
            var constants = ReflectionHelpers.GetConstants(typeof (RegionShippingCost.Regions));

            Assert.That(constants.Count(), Is.EqualTo(3));
        }
    }
}
