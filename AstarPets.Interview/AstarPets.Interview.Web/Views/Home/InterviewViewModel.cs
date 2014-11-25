using System.Collections.Generic;
using AstarPets.Interview.Business.Basket;
using AstarPets.Interview.Business.Shipping;

namespace AstarPets.Interview.Web.Views.Home
{
    public class InterviewViewModel
    {
        public Dictionary<string, ShippingBase> ShippingOptions { get; set; }
        public Basket Basket { get; set; }
    }
}