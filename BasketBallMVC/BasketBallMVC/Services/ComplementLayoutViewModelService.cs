using BasketBallMVC.DAL;
using BasketBallMVC.ViewModel;
using System;
using System.Linq;
using System.Web;

namespace BasketBallMVC.Services
{


    public class ComplementLayoutViewModelService
    {
        public T FillLayoutViewModel<T>() where T : LayoutViewModel, new()
        {
            T obj = new T();

            using (var db = new BasketBallContext())
            {
                var loggedInUser = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                var character = db.Characters.FirstOrDefault(x => x.UserId == loggedInUser.Id);
                var levelValues = db.Levels.FirstOrDefault(x => x.Lvl == character.Level);
                bool notifications = db.Notifications.Any(x => x.user.Id == loggedInUser.Id && x.isOpen == false);
                bool messages = db.Messages.Any(x => x.Addressee.Id == loggedInUser.Id && x.isRead == false);

                if (character.IsBusy && DateTime.Now >= character.BusyEndTime)
                {
                    character.IsBusy = false;
                    db.SaveChanges();
                }
                var prevLevelExp = 0;
                if (character.Level > 1)
                {
                    prevLevelExp = db.Levels.FirstOrDefault(x => x.Lvl == character.Level - 1).MaxExperience;
                }


                obj.Name = loggedInUser.UserData.Nick;
                obj.Gold = character.Gold;
                obj.Health = character.Health;
                obj.Level = character.Level;
                obj.Energy = character.Energy;
                obj.MaxHealth = levelValues.MaxHealth;
                obj.MaxEnergy = levelValues.MaxEnergy;
                obj.EnergyInPercent = 100 * obj.Energy / obj.MaxEnergy;
                obj.HealthInPercent = 100 * obj.Health / obj.MaxHealth;
                obj.LevelInPercent = 100 * (character.Experience - prevLevelExp) / (levelValues.MaxExperience - prevLevelExp);
                obj.Notifications = notifications;
                obj.Messages = messages;
                obj.BusyEndTime = character.BusyEndTime;
                obj.IsBusy = character.IsBusy;
            }
            return obj;
        }
    }
}