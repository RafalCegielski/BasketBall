using BasketBallMVC.DAL;
using BasketBallMVC.Services;
using BasketBallMVC.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BasketBallMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ComplementViewModelsService _complementVMService = new ComplementViewModelsService();
        private UserService _userService = new UserService();
        // GET: Home
        [Authorize]
        public ActionResult Home()
        {
           
            HomeViewModel homeVM = _complementVMService.ComplementHomeVM();

            return View(homeVM);
        }
        public JsonResult GetBusyTime()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var character = _userService.GetCurrentUserCharacter();
            if (character.IsBusy)
            {
                dictionary.Add("isBusy", character.IsBusy);
                dictionary.Add("busyEndTime", character.BusyEndTime);
            }
            else
            {
                dictionary.Add("isBusy", character.IsBusy);
            }
            string json = JsonConvert.SerializeObject(dictionary);

            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}