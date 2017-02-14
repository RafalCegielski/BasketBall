using BasketBallMVC.Services;
using BasketBallMVC.ViewModel;
using System.Web.Mvc;

namespace BasketBallMVC.Controllers
{
    [Authorize]
    public class CityController : Controller
    {
        private ComplementViewModelsService _complementVMService = new ComplementViewModelsService();
        private RestaurantService _restaurantService = new RestaurantService();
        private ShopService _shopService = new ShopService();
        

        public ActionResult Shop()
        {
            ShopViewModel shopVM = _complementVMService.ComplementShopVM();
            
            return View(shopVM);
        }

        public JsonResult GetCtegoriesForShop(string buttonId)
        {
            string json = _shopService.GetCategoriesForShop(buttonId);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuyNewDevice(string buttonId)
        {
            string json = _shopService.BuyNewDevice(buttonId);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public void SaleDevice(string buttonId)
        {
            _shopService.SaleDevice(buttonId);
        }

        public ActionResult Restaurant()
        {
            RestaurantViewModel restaurantVM = _complementVMService.ComplementRestaurantVM();

            return View(restaurantVM);
        }

        public void BuyFood(string btnId, string size)
        {
            _restaurantService.BuyFood(btnId, size);
        }
    }
}