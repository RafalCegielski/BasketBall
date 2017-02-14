using BasketBallMVC.DAL;
using BasketBallMVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketBallMVC.Services
{
    public class CharacterService
    {
        private static bool _flag = true;

        public void ChangeStats(List<BattleLog> battleLog, bool isComputerMode, string oponentId)
        {
            using (var db = new BasketBallContext())
            {
                var oponentUser = db.Users.Find(oponentId);
                Character oponentCharacter = new Character();
                Level oponentLevel = new Level();
                int oponentMaxExp = 0;
                int oponentMaxEnergy = 0;
                if (!isComputerMode)
                {
                    oponentCharacter = db.Characters.FirstOrDefault(x => x.UserId == oponentUser.Id);
                    oponentLevel = db.Levels.FirstOrDefault(x => x.Lvl == oponentCharacter.Level);

                    oponentMaxExp = db.Levels.FirstOrDefault(x => x.Lvl == oponentCharacter.Level).MaxExperience;

                    oponentMaxEnergy = db.Levels.FirstOrDefault(x => x.Lvl == oponentCharacter.Level).MaxEnergy;
                }

                var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);
                var level = db.Levels.FirstOrDefault(x => x.Lvl == character.Level);

                var PlayerResult = int.Parse(battleLog[battleLog.Count - 1].leftSum);
                var OponentResult = int.Parse(battleLog[battleLog.Count - 1].rightSum);


                var maxExp = db.Levels.FirstOrDefault(x => x.Lvl == character.Level).MaxExperience;
                var maxEnergy = db.Levels.FirstOrDefault(x => x.Lvl == character.Level).MaxEnergy;

                if (PlayerResult > OponentResult)
                {
                    if (isComputerMode)
                    {
                        character.Experience += (80 * character.Level);
                        if (character.Experience > level.MaxExperience)
                            character.Level++;
                        character.Gold += 30 + character.Level * 10;

                    }
                    else
                    {
                        character.Experience += (100 * character.Level);
                        oponentCharacter.Experience += (50 * character.Level);
                        if (character.Experience > level.MaxExperience)
                            character.Level++;
                        if (oponentCharacter.Experience > oponentLevel.MaxExperience)
                            oponentCharacter.Level++;
                        character.Gold += 50 + character.Level * 15;
                        oponentCharacter.Gold += 15 + oponentCharacter.Level * 8;
                        character.WinGames++;
                        character.GameCounter++;
                        oponentCharacter.LoseGames++;
                        oponentCharacter.GameCounter++;
                    }

                }
                else if (PlayerResult < OponentResult)
                {
                    if (isComputerMode)
                    {
                        character.Experience += (50 * character.Level);
                        if (character.Experience > level.MaxExperience)
                            character.Level++;
                        character.Gold += 10 + character.Level * 5;
                    }
                    else
                    {
                        character.Experience += (80 * character.Level);
                        oponentCharacter.Experience += (80 * oponentCharacter.Level);
                        if (character.Experience > level.MaxExperience)
                            character.Level++;
                        if (oponentCharacter.Experience > oponentLevel.MaxExperience)
                            oponentCharacter.Level++;
                        character.Gold += 15 + character.Level * 8;
                        oponentCharacter.Gold += 50 + oponentCharacter.Level * 15;
                        character.LoseGames++;
                        character.GameCounter++;
                        oponentCharacter.WinGames++;
                        oponentCharacter.GameCounter++;
                    }
                }
                else
                {
                    if (isComputerMode)
                    {
                        character.Experience += (30 * character.Level);
                        if (character.Experience > level.MaxExperience)
                            character.Level++;
                        character.Gold += 20 + character.Level * 6;
                    }
                    else
                    {
                        character.Experience += (50 * character.Level);
                        oponentCharacter.Experience += (100 * character.Level);
                        if (character.Experience > level.MaxExperience)
                            character.Level++;
                        if (oponentCharacter.Experience > oponentLevel.MaxExperience)
                            oponentCharacter.Level++;
                        character.Gold += 25 + character.Level * 10;
                        oponentCharacter.Gold += 25 + oponentCharacter.Level * 10;
                        character.GameCounter++;
                        oponentCharacter.GameCounter++;
                    }
                }
                character.Energy -= (20 + maxEnergy * 5 / 100);
                if (!isComputerMode)
                {
                    oponentCharacter.Energy -= (20 + oponentMaxEnergy * 5 / 100);
                    if (oponentCharacter.Energy < 0)
                        oponentCharacter.Energy = 0;
                }


                db.SaveChanges();
            }
        }

        public void UpdateStatsDaily()
        {
            using (var db = new BasketBallContext())
            {
                var levels = db.Levels.ToList();
                foreach (var item in db.Characters)
                {
                    var currentLevel = levels.FirstOrDefault(x => x.Lvl == item.Level);
                    item.Health = currentLevel.MaxHealth;
                    item.Energy = currentLevel.MaxEnergy;
                    item.Gold += 50;
                }
                db.SaveChanges();
            }
        }

        public void RankingUpdater()
        {
            if (_flag)
            {
                _flag = false;
                using (var db = new BasketBallContext())
                {
                    var allCharacters = db.Characters.OrderByDescending(x => x.Experience * 2 + x.WinGames * 7).ToList();
                    int i = 1;
                    foreach (var item in allCharacters)
                    {
                        item.RankingPosition = i++;
                    }
                    db.SaveChanges();
                }
                _flag = true;
            }
        }

        public void CreateCharacter(string userId)
        {
            using (var db = new BasketBallContext())
            {
                Character newCharacter = new Character
                {
                    CharacterID = Guid.NewGuid(),
                    Defence = 0,
                    Experience = 0,
                    LoseGames = 0,
                    Marksmanship = 0,
                    Strengh = 0,
                    UserId = userId,
                    WinGames = 0,
                    Energy = 100,
                    Gold = 150,
                    Health = 200,
                    Level = 1,
                    Speed = 0,
                    GameCounter = 0,
                    gameStyle = 50,
                    IsBusy = false,
                    BusyEndTime = DateTime.Now.AddDays(-2)
                };
                db.Characters.Add(newCharacter);
                TraningRoom traningRoom = new TraningRoom { Character = newCharacter, TraningRoomID = Guid.NewGuid() };
                List<TraningRoomByExercise> traningRoomByExercise = new List<TraningRoomByExercise>();
                var categories = db.ExerciseCategories.ToList();

                foreach (var item in categories)
                {
                    var exercise = db.Exercises.FirstOrDefault(x => x.ExerciseCategory.Name == item.Name && x.Level == 0);
                    traningRoomByExercise.Add(new TraningRoomByExercise { ExerciseAtack = exercise, ExerciseDefence = exercise, ExerciseCategoryName = item.Name, IsCompleteAtack = true, IsCompleteDefence = true, TraningRoom = traningRoom, TraningRoomByExerciseID = Guid.NewGuid(), AtackTrainingFinish = DateTime.Now, DefenceTrainingFinish = DateTime.Now });
                    db.SaveChanges();
                }
                traningRoom.TraningRoomByExercises = traningRoomByExercise;
                traningRoom.TraningRoomByDevices = new List<TraningRoomByDevice>();

                db.TrainingRooms.Add(traningRoom);
                db.TraningRoomByExercises.AddRange(traningRoomByExercise);
                db.SaveChanges();
            }
        }

        public Tuple<bool, bool> CheckLifeAndEnergy()
        {
            Tuple<bool, bool> tuple;
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                var character = db.Characters
                    .FirstOrDefault(x => x.UserId == user.Id);
                var maxEnergy = db.Levels
                    .FirstOrDefault(x => x.Lvl == character.Level)
                    .MaxEnergy;
                int addMaxEnergy = maxEnergy * 5 / 100;
                bool isEnoughLife = (character.Health > 0) ? true : false;
                bool isEnoughEnergy = (character.Energy > 20 + addMaxEnergy) ? true : false;
                tuple = new Tuple<bool, bool>(isEnoughLife, isEnoughEnergy);
            }
            return tuple;
        }
    }
}