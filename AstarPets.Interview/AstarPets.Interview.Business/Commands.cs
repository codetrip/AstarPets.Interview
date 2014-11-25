using AstarPets.Interview.Business.Basket;
using AstarPets.Interview.Business.Core;

namespace AstarPets.Interview.Business
{
    public interface IRemoveFromBasketCommand : ICommand<int, bool>{}
    public interface IAddToBasketCommand : ICommand<AddToBasketRequest, AddToBasketResponse>{}
}