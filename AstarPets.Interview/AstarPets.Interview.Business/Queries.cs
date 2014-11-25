using AstarPets.Interview.Business.Basket;
using AstarPets.Interview.Business.Core;
using AstarPets.Interview.Business.Shipping;

namespace AstarPets.Interview.Business
{
    public interface IGetBasketQuery : IQuery<BasketRequest, Basket.Basket>{}
    public interface IGetShippingOptionsQuery: IQuery<GetShippingOptionsRequest, GetShippingOptionsResponse>{}
}