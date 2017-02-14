using System.Web.Mvc;

namespace BasketBallMVC.Controllers
{
    [Authorize]
    public class StartingPageController : Controller
    {
        // GET: StartingPage
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}