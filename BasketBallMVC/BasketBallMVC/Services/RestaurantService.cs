using BasketBallMVC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketBallMVC.Services
{
    public class RestaurantService
    {
        public void BuyFood(string btnId, string size)
        {
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);
                var level = db.Levels.FirstOrDefault(x => x.Lvl == character.Level);
                int gold = character.Gold;

                int smallCost = 50 + character.Level * 10;
                int mediumCost = 60 + character.Level * 20;
                int bigCost = 100 + character.Level * 30;

                btnId = btnId.Replace("BuyBtn", "");
                bool isAvailible = false;
                int cost = 0;
                switch (size)
                {
                    case "20":
                        if (gold >= smallCost)
                            isAvailible = true;
                        cost = smallCost;
                        break;
                    case "60":
                        if (gold >= mediumCost)
                            isAvailible = true;
                        cost = mediumCost;
                        break;
                    case "100":
                        if (gold >= bigCost)
                            isAvailible = true;
                        cost = bigCost;
                        break;
                }

                if (isAvailible)
                {
                    character.Gold -= cost;

                    if (btnId == "Energy")
                    {
                        if (smallCost == cost)
                        {
                            var energy = character.Energy + level.MaxEnergy * 20 / 100;
                            if (energy > level.MaxEnergy)
                                character.Energy = level.MaxEnergy;
                            else
                                character.Energy += energy;
                        }
                        else if (mediumCost == cost)
                        {
                            var energy = character.Energy + level.MaxEnergy * 60 / 100;
                            if (energy > level.MaxEnergy)
                                character.Energy = level.MaxEnergy;
                            else
                                character.Energy += energy;
                        }
                        else
                        {
                            character.Energy = level.MaxEnergy;
                        }
                    }
                    else if (btnId == "Health")
                    {
                        if (smallCost == cost)
                        {
                            var health = character.Health + level.MaxHealth * 20 / 100;
                            if (health > level.MaxHealth)
                                character.Health = level.MaxHealth;
                            else
                                character.Health += health;
                        }
                        else if (mediumCost == cost)
                        {
                            var health = character.Health + level.MaxHealth * 60 / 100;
                            if (health > level.MaxHealth)
                                character.Health = level.MaxHealth;
                            else
                                character.Health += health;
                        }
                        else
                        {
                            character.Health = level.MaxHealth;
                        }
                    }
                }
                db.SaveChanges();
            }
        }
    }
}