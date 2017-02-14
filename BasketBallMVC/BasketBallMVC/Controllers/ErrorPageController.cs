using System.Web.Mvc;

namespace BasketBallMVC.Controllers
{
    public class ErrorPageController : Controller
    {
        // GET: ErrorPage
        public ActionResult ErrorMessage()
        {
            return View();
        }
    }
}