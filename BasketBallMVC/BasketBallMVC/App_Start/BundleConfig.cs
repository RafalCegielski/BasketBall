using System.Web.Optimization;

namespace BasketBallMVC.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
               "~/Scripts/jquery-{version}.js"
               ));
            bundles.Add(new StyleBundle("~/bundles/startingPage").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/StartingPage.css"
                //"~/Content/Reset.css"
               ));
            bundles.Add(new StyleBundle("~/bundles/trainingRoom").Include(
                "~/Content/ProgressBar.css",
                "~/Content/TraingingRoom.css"
               ));
            bundles.Add(new StyleBundle("~/bundles/battle").Include(
                "~/Content/Battle.css"
               ));
            bundles.Add(new StyleBundle("~/bundles/gameStyle").Include(
                "~/Content/bootstrap-slider.min.css",
                "~/Content/GameStyle.css"
               ));
            bundles.Add(new StyleBundle("~/bundles/adminUser").Include(
                "~/Content/AdminUser.css"
               ));
            bundles.Add(new StyleBundle("~/bundles/notification").Include(
                "~/Content/Notifications.css"
               ));
            bundles.Add(new StyleBundle("~/bundles/battleSettings").Include(
                "~/Content/GameSettings.css"
               ));
            bundles.Add(new StyleBundle("~/bundles/shop").Include(
                "~/Content/Shop.css"
               ));
            bundles.Add(new StyleBundle("~/bundles/restaurant").Include(
                "~/Content/ProgressBar.css",
                "~/Content/Restaurant.css"
              ));

            bundles.Add(new StyleBundle("~/bundles/Layout").Include(
                "~/Content/MainStyle.css",
                "~/Content/menu.css",
                "~/Content/bootstrap.css"
              ));

            bundles.Add(new ScriptBundle("~/Scripts/LayoutScripts").Include(
                "~/Scripts/jquery-1.9.1.js",
                "~/Scripts/bootstrap.js" ,
                "~/Scripts/MenuResize.js" ,
                "~/Scripts/jquery.signalR-2.2.0.js",
                "~/Scripts/bootbox.js",
                "~/Scripts/BusyTime.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/Messages").Include(
                "~/Scripts/Messages.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/Scripts").Include(
               "~/Scripts/ShopScripts.js",
               "~/Scripts/Scripts.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/trainingRoomScripts").Include(
               "~/Scripts/TrainingRoom.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/battleSettingsScripts").Include(
                "~/Scripts/GameSettings.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/restaurantScripts").Include(
                "~/Scripts/Restaurant.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/notificationScripts").Include(
                "~/Scripts/Notification.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/adminUserScripts").Include(
                "~/Scripts/AdminUser.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/gameStyleScripts").Include(
                "~/Scripts/bootstrap-slider.min.js",
                "~/Scripts/GameStyle.js"
               ));
        }
    }
}