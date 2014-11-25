using System.Web;
using System.Xml.Linq;

namespace AstarPets.Interview.Business.Basket
{
    public class GetBasketQuery : BasketOperationBase, IGetBasketQuery
    {
        private readonly IShippingCalculator _shippingCalculator;

        public GetBasketQuery()
        {
            _shippingCalculator = new ShippingCalculator();
        }

        public Basket Invoke(BasketRequest request)
        {
            var basket = GetBasket();
            basket.Shipping = _shippingCalculator.CalculateShipping(basket);

            return basket;
        }
    }

    public class BasketRequest { }
}