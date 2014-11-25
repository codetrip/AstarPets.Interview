using System.Web.Mvc;
using AstarPets.Interview.Business;
using AstarPets.Interview.Business.Basket;
using AstarPets.Interview.Business.Shipping;
using AstarPets.Interview.Web.Views.Home;

namespace AstarPets.Interview.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IGetBasketQuery _basketLoader;
        private readonly IAddToBasketCommand _addToBasket;
        private readonly IGetShippingOptionsQuery _getShippingOptions;
        private readonly IRemoveFromBasketCommand _removeFromBasketCommand;

        public HomeController(IGetBasketQuery basketLoader, 
            IAddToBasketCommand addToBasket, 
            IGetShippingOptionsQuery getShippingOptions, 
            IRemoveFromBasketCommand removeFromBasketCommand)
        {
            _basketLoader = basketLoader;
            _addToBasket = addToBasket;
            _getShippingOptions = getShippingOptions;
            _removeFromBasketCommand = removeFromBasketCommand;
        }

        public ActionResult Index()
        {
            var basket = _basketLoader.Invoke(new BasketRequest());
            var shippingOptions = _getShippingOptions.Invoke(new GetShippingOptionsRequest()).ShippingOptions;

            var viewModel = new InterviewViewModel {Basket = basket, ShippingOptions = shippingOptions};

            return View(viewModel);
        }

        public ActionResult RemoveItem(int id)
        {
            _removeFromBasketCommand.Invoke(id);

            return RedirectToAction("Index");
        }

        public ActionResult AddItem(LineItemViewModel lineItemViewModel)
        {
            var shippingOptions = _getShippingOptions.Invoke(new GetShippingOptionsRequest()).ShippingOptions;

            var lineItem = new LineItem()
                               {
                                   Amount = lineItemViewModel.Amount,
                                   ProductId = lineItemViewModel.ProductId,
                                   Shipping = shippingOptions[lineItemViewModel.ShippingOption],
                                   SupplierId = lineItemViewModel.SupplierId,
                                   DeliveryRegion = lineItemViewModel.DeliveryRegion,
                               };

            _addToBasket.Invoke(new AddToBasketRequest() {LineItem = lineItem});

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
