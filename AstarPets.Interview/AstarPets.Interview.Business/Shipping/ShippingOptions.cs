using System;
using System.Collections.Generic;
using System.IO;
using AstarPets.Interview.Business.Core;

namespace AstarPets.Interview.Business.Shipping
{
    public class GetShippingOptions : IGetShippingOptionsQuery
    {
        public GetShippingOptionsResponse Invoke(GetShippingOptionsRequest request)
        {
            using (var sr = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\Shipping.xml")))
            {
                var ser = sr.ReadToEnd();
                var response = new GetShippingOptionsResponse()
                                   {
                                       ShippingOptions =
                                           SerializationHelper.
                                           DataContractDeserialize<Dictionary<string, ShippingBase>>(ser)
                                   };

                return response;
            }
        }
    }

    public class GetShippingOptionsResponse
    {
        public Dictionary<string, ShippingBase> ShippingOptions { get; set; }
    }

    public class GetShippingOptionsRequest
    {
    }
}