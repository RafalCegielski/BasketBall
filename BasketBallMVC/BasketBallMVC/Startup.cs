using BasketBallMVC.Services;
using Hangfire;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//[assembly: OwinStartup(typeof(BasketBallMVC.Startup))]
namespace BasketBallMVC
{
    public partial class Startup
    {
        CharacterService _characterService = new CharacterService();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.MapSignalR();

            #region hangfire
            GlobalConfiguration.Configuration.UseSqlServerStorage("BasketBallContext");
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            RecurringJob.AddOrUpdate(
                () => _characterService.RankingUpdater(), Cron.MinuteInterval(2)
                );
            RecurringJob.AddOrUpdate(
                () => _characterService.UpdateStatsDaily(), Cron.Daily
                );
            #endregion


        }
    }
}