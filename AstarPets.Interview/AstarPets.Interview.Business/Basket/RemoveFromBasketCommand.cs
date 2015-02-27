using System.Linq;

namespace AstarPets.Interview.Business.Basket
{
    public class RemoveFromBasketCommand : BasketOperationBase, IRemoveFromBasketCommand
    {
        public bool Invoke(int id)
        {
            var basket = GetBasket();

            basket.LineItems.RemoveWhere(li => li.Id == id);

            SaveBasket(basket);

            return true;
        }
    }
}