using BasketBallMVC.App_Start;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BasketBallMVC
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Database.SetInitializer<BasketBallContext>(new BasketBallInitializer()); //Commented Initializer
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exp = Server.GetLastError();
            Server.ClearError();
            Response.Redirect("/Home/Home");
        }
    }
}
