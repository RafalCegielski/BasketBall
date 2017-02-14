using BasketBallMVC.DAL;
using BasketBallMVC.Model;
using BasketBallMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketBallMVC.Services
{
    public class BattleService
    {
        Random r = new Random();
        public List<BattleHelper> BattleVsPlayer(string oponentId)
        {
            List<BattleHelper> atacksLog = new List<BattleHelper>();
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);
                var oponentUser = db.Users.Find(oponentId);
                var oponent = db.Characters.FirstOrDefault(x => x.UserId == oponentUser.Id);

                int currentUserFaulChance = 0;
                int currentUserAdditionalSuccessChance = 0;
                int oponentFaulChance = 0;
                int oponentAdditionalSuccessChance = 0;

                switch (character.gameStyle)
                {
                    case 0:
                        currentUserFaulChance = 0;
                        currentUserAdditionalSuccessChance = 0;
                        break;
                    case 50:
                        currentUserFaulChance = 5;
                        currentUserAdditionalSuccessChance = 10;
                        break;
                    case 100:
                        currentUserFaulChance = 20;
                        currentUserAdditionalSuccessChance = 30;
                        break;
                }
                switch (oponent.gameStyle)
                {
                    case 0:
                        oponentFaulChance = 0;
                        oponentAdditionalSuccessChance = 0;
                        break;
                    case 50:
                        oponentFaulChance = 5;
                        oponentAdditionalSuccessChance = 10;
                        break;
                    case 100:
                        oponentFaulChance = 20;
                        oponentAdditionalSuccessChance = 30;
                        break;
                }



                //character atack
                int faulCounter = 0;
                bool isSuccess;
                var characterAtack = db.Exercises
                    .Find(character.chosenAttacks.Exercise1);
                var oponentDef = oponent.TraningRoom.TraningRoomByExercises
                    .FirstOrDefault(x => x.ExerciseCategoryName == characterAtack.ExerciseCategory.Name).ExerciseDefence;
                var accurateThrow = (characterAtack.Level * 20) + character.Strengh +
                    character.Defence + character.Speed + character.Marksmanship + 1200 -
                    oponent.Speed - oponent.Defence - oponent.Strengh -
                    oponent.Marksmanship - (oponentDef.Level * 20);
                accurateThrow /= 22;
                accurateThrow += currentUserAdditionalSuccessChance;
                bool isFaul = Rand(currentUserFaulChance);
                if (isFaul)
                {
                    characterAtack = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == "Rzut wolny").ExerciseAtack;
                    oponentDef = oponent.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == characterAtack.ExerciseCategory.Name).ExerciseDefence;

                    faulCounter = 0;

                    accurateThrow = (characterAtack.Level * 20) + oponent.Strengh + oponent.Defence + oponent.Speed + oponent.Marksmanship + 1200 - character.Speed - character.Defence - character.Strengh - character.Marksmanship - (oponentDef.Level * 20);
                    accurateThrow /= 22;
                    accurateThrow += oponentAdditionalSuccessChance;
                    isSuccess = Rand(accurateThrow);

                    if (isSuccess)
                    {
                        faulCounter++;
                    }
                    accurateThrow = (characterAtack.Level * 20) + oponent.Strengh + oponent.Defence + oponent.Speed + oponent.Marksmanship + 1200 - character.Speed - character.Defence - character.Strengh - character.Marksmanship - (oponentDef.Level * 20);
                    accurateThrow /= 22;
                    accurateThrow += oponentAdditionalSuccessChance;
                    isSuccess = Rand(accurateThrow);

                    if (isSuccess)
                    {
                        faulCounter++;
                    }
                    atacksLog.Add(new BattleHelper { Atack = oponentDef.ExerciseCategory.Name, Nick = oponentUser.UserData.Nick, isSuccess = isSuccess, NickOponent = user.UserData.Nick, isAdditionalAtack = true, additionalAtackResult = faulCounter });
                }
                else
                {
                    isSuccess = Rand(accurateThrow);
                    if (!isSuccess)
                    {
                        if (character.Health > 20)
                            character.Health -= 20;
                        else
                            character.Health = 0;
                    }
                    else
                    {
                        if (oponent.Health > 20)
                            oponent.Health -= 20;
                        else
                            oponent.Health = 0;
                    }
                    atacksLog.Add(new BattleHelper { Atack = oponentDef.ExerciseCategory.Name, Nick = user.UserData.Nick, isSuccess = isSuccess, NickOponent = oponentUser.UserData.Nick, isAdditionalAtack = false });
                }


                //oponent atack
                characterAtack = db.Exercises.Find(oponent.chosenAttacks.Exercise1);
                oponentDef = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == characterAtack.ExerciseCategory.Name).ExerciseDefence;
                accurateThrow = (characterAtack.Level * 20) + oponent.Strengh + oponent.Defence + oponent.Speed + oponent.Marksmanship + 1200 - character.Speed - character.Defence - character.Strengh - character.Marksmanship - (oponentDef.Level * 20);
                accurateThrow /= 22;
                accurateThrow += oponentAdditionalSuccessChance;
                isFaul = Rand(oponentFaulChance);
                if (isFaul)
                {
                    characterAtack = oponent.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == "Rzut wolny").ExerciseAtack;
                    oponentDef = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == characterAtack.ExerciseCategory.Name).ExerciseDefence; // my

                    faulCounter = 0;

                    accurateThrow = (characterAtack.Level * 20) + character.Strengh + character.Defence + character.Speed + character.Marksmanship + 1200 - oponent.Speed - oponent.Defence - oponent.Strengh - oponent.Marksmanship - (oponentDef.Level * 20);
                    accurateThrow /= 22;
                    accurateThrow += oponentAdditionalSuccessChance;
                    isSuccess = Rand(accurateThrow);

                    if (isSuccess)
                    {
                        faulCounter++;
                    }
                    accurateThrow = (characterAtack.Level * 20) + character.Strengh + character.Defence + character.Speed + character.Marksmanship + 1200 - oponent.Speed - oponent.Defence - oponent.Strengh - oponent.Marksmanship - (oponentDef.Level * 20);
                    accurateThrow /= 22;
                    accurateThrow += oponentAdditionalSuccessChance;
                    isSuccess = Rand(accurateThrow);

                    if (isSuccess)
                    {
                        faulCounter++;
                    }
                    atacksLog.Add(new BattleHelper { Atack = oponentDef.ExerciseCategory.Name, Nick = user.UserData.Nick, isSuccess = isSuccess, NickOponent = oponentUser.UserData.Nick, isAdditionalAtack = true, additionalAtackResult = faulCounter });
                }
                else
                {
                    isSuccess = Rand(accurateThrow);
                    if (!isSuccess)
                    {
                        if (oponent.Health > 20)
                            oponent.Health -= 20;
                        else
                            oponent.Health = 0;
                    }
                    else
                    {
                        if (character.Health > 20)
                            character.Health -= 20;
                        else
                            character.Health = 0;
                    }
                    atacksLog.Add(new BattleHelper { Atack = oponentDef.ExerciseCategory.Name, Nick = oponentUser.UserData.Nick, isSuccess = isSuccess, NickOponent = user.UserData.Nick, isAdditionalAtack = false });
                }

                //character atack
                characterAtack = db.Exercises.Find(character.chosenAttacks.Exercise2);
                oponentDef = oponent.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == characterAtack.ExerciseCategory.Name).ExerciseDefence;
                accurateThrow = (characterAtack.Level * 20) + character.Strengh + character.Defence + character.Speed + character.Marksmanship + 1200 - oponent.Speed - oponent.Defence - oponent.Strengh - oponent.Marksmanship - (oponentDef.Level * 20);
                accurateThrow /= 22;
                accurateThrow += currentUserAdditionalSuccessChance;
                isFaul = Rand(currentUserFaulChance);
                if (isFaul)
                {
                    characterAtack = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == "Rzut wolny").ExerciseAtack;
                    oponentDef = oponent.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == characterAtack.ExerciseCategory.Name).ExerciseDefence;

                    faulCounter = 0;

                    accurateThrow = (characterAtack.Level * 20) + oponent.Strengh + oponent.Defence + oponent.Speed + oponent.Marksmanship + 1200 - character.Speed - character.Defence - character.Strengh - character.Marksmanship - (oponentDef.Level * 20);
                    accurateThrow /= 22;
                    accurateThrow += oponentAdditionalSuccessChance;
                    isSuccess = Rand(accurateThrow);

                    if (isSuccess)
                    {
                        faulCounter++;
                    }
                    accurateThrow = (characterAtack.Level * 20) + oponent.Strengh + oponent.Defence + oponent.Speed + oponent.Marksmanship + 1200 - character.Speed - character.Defence - character.Strengh - character.Marksmanship - (oponentDef.Level * 20);
                    accurateThrow /= 22;
                    accurateThrow += oponentAdditionalSuccessChance;
                    isSuccess = Rand(accurateThrow);

                    if (isSuccess)
                    {
                        faulCounter++;
                    }
                    atacksLog.Add(new BattleHelper { Atack = oponentDef.ExerciseCategory.Name, Nick = oponentUser.UserData.Nick, isSuccess = isSuccess, NickOponent = user.UserData.Nick, isAdditionalAtack = true, additionalAtackResult = faulCounter });
                }
                else
                {
                    isSuccess = Rand(accurateThrow);
                    if (!isSuccess)
                    {
                        if (character.Health > 20)
                            character.Health -= 20;
                        else
                            character.Health = 0;
                    }
                    else
                    {
                        if (oponent.Health > 20)
                            oponent.Health -= 20;
                        else
                            oponent.Health = 0;
                    }
                    atacksLog.Add(new BattleHelper { Atack = oponentDef.ExerciseCategory.Name, Nick = user.UserData.Nick, isSuccess = isSuccess, NickOponent = oponentUser.UserData.Nick, isAdditionalAtack = false });
                }

                //oponent atack
                characterAtack = db.Exercises.Find(oponent.chosenAttacks.Exercise2);
                oponentDef = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == characterAtack.ExerciseCategory.Name).ExerciseDefence;
                accurateThrow = (characterAtack.Level * 20) + oponent.Strengh + oponent.Defence + oponent.Speed + oponent.Marksmanship + 1200 - character.Speed - character.Defence - character.Strengh - character.Marksmanship - (oponentDef.Level * 20);
                accurateThrow /= 22;
                accurateThrow += oponentAdditionalSuccessChance;
                isFaul = Rand(oponentFaulChance);
                if (isFaul)
                {

                    characterAtack = oponent.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == "Rzut wolny").ExerciseAtack;
                    oponentDef = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == characterAtack.ExerciseCategory.Name).ExerciseDefence; // my

                    faulCounter = 0;

                    accurateThrow = (characterAtack.Level * 20) + character.Strengh + character.Defence + character.Speed + character.Marksmanship + 1200 - oponent.Speed - oponent.Defence - oponent.Strengh - oponent.Marksmanship - (oponentDef.Level * 20);
                    accurateThrow /= 22;
                    accurateThrow += oponentAdditionalSuccessChance;
                    isSuccess = Rand(accurateThrow);

                    if (isSuccess)
                    {
                        faulCounter++;
                    }
                    accurateThrow = (characterAtack.Level * 20) + character.Strengh + character.Defence + character.Speed + character.Marksmanship + 1200 - oponent.Speed - oponent.Defence - oponent.Strengh - oponent.Marksmanship - (oponentDef.Level * 20);
                    accurateThrow /= 22;
                    accurateThrow += oponentAdditionalSuccessChance;
                    isSuccess = Rand(accurateThrow);

                    if (isSuccess)
                    {
                        faulCounter++;
                    }
                    atacksLog.Add(new BattleHelper { Atack = oponentDef.ExerciseCategory.Name, Nick = user.UserData.Nick, isSuccess = isSuccess, NickOponent = oponentUser.UserData.Nick, isAdditionalAtack = true, additionalAtackResult = faulCounter });


                }
                else
                {
                    isSuccess = Rand(accurateThrow);
                    if (!isSuccess)
                    {
                        if (oponent.Health > 20)
                            oponent.Health -= 20;
                        else
                            oponent.Health = 0;
                    }
                    else
                    {
                        if (character.Health > 20)
                            character.Health -= 20;
                        else
                            character.Health = 0;
                    }
                    atacksLog.Add(new BattleHelper { Atack = oponentDef.ExerciseCategory.Name, Nick = oponentUser.UserData.Nick, isSuccess = isSuccess, NickOponent = user.UserData.Nick });
                }

                //character atack
                characterAtack = db.Exercises.Find(character.chosenAttacks.Exercise3);
                oponentDef = oponent.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == characterAtack.ExerciseCategory.Name).ExerciseDefence;
                accurateThrow = (characterAtack.Level * 20) + character.Strengh + character.Defence + character.Speed + character.Marksmanship + 1200 - oponent.Speed - oponent.Defence - oponent.Strengh - oponent.Marksmanship - (oponentDef.Level * 20);
                accurateThrow /= 22;
                accurateThrow += currentUserAdditionalSuccessChance;
                isFaul = Rand(currentUserFaulChance);
                if (isFaul)
                {
                    characterAtack = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == "Rzut wolny").ExerciseAtack;
                    oponentDef = oponent.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == characterAtack.ExerciseCategory.Name).ExerciseDefence;

                    faulCounter = 0;

                    accurateThrow = (characterAtack.Level * 20) + oponent.Strengh + oponent.Defence + oponent.Speed + oponent.Marksmanship + 1200 - character.Speed - character.Defence - character.Strengh - character.Marksmanship - (oponentDef.Level * 20);
                    accurateThrow /= 22;
                    accurateThrow += oponentAdditionalSuccessChance;
                    isSuccess = Rand(accurateThrow);

                    if (isSuccess)
                    {
                        faulCounter++;
                    }
                    accurateThrow = (characterAtack.Level * 20) + oponent.Strengh + oponent.Defence + oponent.Speed + oponent.Marksmanship + 1200 - character.Speed - character.Defence - character.Strengh - character.Marksmanship - (oponentDef.Level * 20);
                    accurateThrow /= 22;
                    accurateThrow += oponentAdditionalSuccessChance;
                    isSuccess = Rand(accurateThrow);

                    if (isSuccess)
                    {
                        faulCounter++;
                    }
                    atacksLog.Add(new BattleHelper { Atack = oponentDef.ExerciseCategory.Name, Nick = oponentUser.UserData.Nick, isSuccess = isSuccess, NickOponent = user.UserData.Nick, isAdditionalAtack = true, additionalAtackResult = faulCounter });
                }
                else
                {
                    isSuccess = Rand(accurateThrow);
                    if (!isSuccess)
                    {
                        if (character.Health > 20)
                            character.Health -= 20;
                        else
                            character.Health = 0;
                    }
                    else
                    {
                        if (oponent.Health > 20)
                            oponent.Health -= 20;
                        else
                            oponent.Health = 0;
                    }
                    atacksLog.Add(new BattleHelper { Atack = oponentDef.ExerciseCategory.Name, Nick = user.UserData.Nick, isSuccess = isSuccess, NickOponent = oponentUser.UserData.Nick, isAdditionalAtack = false });
                }

                //oponent atack
                characterAtack = db.Exercises.Find(oponent.chosenAttacks.Exercise3);
                oponentDef = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == characterAtack.ExerciseCategory.Name).ExerciseDefence;
                accurateThrow = (characterAtack.Level * 20) + oponent.Strengh + oponent.Defence + oponent.Speed + oponent.Marksmanship + 1200 - character.Speed - character.Defence - character.Strengh - character.Marksmanship - (oponentDef.Level * 20);
                accurateThrow /= 22;
                accurateThrow += oponentAdditionalSuccessChance;
                isFaul = Rand(oponentFaulChance);
                if (isFaul)
                {

                    characterAtack = oponent.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == "Rzut wolny").ExerciseAtack;
                    oponentDef = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == characterAtack.ExerciseCategory.Name).ExerciseDefence; // my

                    faulCounter = 0;

                    accurateThrow = (characterAtack.Level * 20) + character.Strengh + character.Defence + character.Speed + character.Marksmanship + 1200 - oponent.Speed - oponent.Defence - oponent.Strengh - oponent.Marksmanship - (oponentDef.Level * 20);
                    accurateThrow /= 22;
                    accurateThrow += oponentAdditionalSuccessChance;
                    isSuccess = Rand(accurateThrow);

                    if (isSuccess)
                    {
                        faulCounter++;
                    }
                    accurateThrow = (characterAtack.Level * 20) + character.Strengh + character.Defence + character.Speed + character.Marksmanship + 1200 - oponent.Speed - oponent.Defence - oponent.Strengh - oponent.Marksmanship - (oponentDef.Level * 20);
                    accurateThrow /= 22;
                    accurateThrow += oponentAdditionalSuccessChance;
                    isSuccess = Rand(accurateThrow);

                    if (isSuccess)
                    {
                        faulCounter++;
                    }
                    atacksLog.Add(new BattleHelper { Atack = oponentDef.ExerciseCategory.Name, Nick = user.UserData.Nick, isSuccess = isSuccess, NickOponent = oponentUser.UserData.Nick, isAdditionalAtack = true, additionalAtackResult = faulCounter });


                }
                else
                {
                    isSuccess = Rand(accurateThrow);
                    if (!isSuccess)
                    {
                        if (oponent.Health > 20)
                            oponent.Health -= 20;
                        else
                            oponent.Health = 0;
                    }
                    else
                    {
                        if (character.Health > 20)
                            character.Health -= 20;
                        else
                            character.Health = 0;
                    }
                    atacksLog.Add(new BattleHelper { Atack = oponentDef.ExerciseCategory.Name, Nick = oponentUser.UserData.Nick, isSuccess = isSuccess, NickOponent = user.UserData.Nick });
                }

                db.SaveChanges();
            }
            return atacksLog;
        }

        public string SearchOponent()
        {
            Character oponent = new Character();
            string id = string.Empty;
            using (var db = new BasketBallContext())
            {
                var user = db.Users.
                    FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                var character = db.Characters
                    .FirstOrDefault(x => x.UserId == user.Id);
                int temp = 0;
                do
                {
                    oponent = db.Characters
                        .FirstOrDefault(x => x.chosenAttacks != null &&
                        x.Level == character.Level + temp &&
                        x.CharacterID != character.CharacterID);
                    if (oponent == null)
                        oponent = db.Characters
                            .FirstOrDefault(x => x.chosenAttacks != null &&
                            x.Level == character.Level - temp &&
                            x.CharacterID != character.CharacterID);
                    temp++;
                } while (oponent == null);
            }
            return oponent.UserId;
        }


        public List<BattleHelper> GenerateBattleVsComputer()
        {
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);
                var exerciseCategoriesList = db.ExerciseCategories.ToList();
                exerciseCategoriesList.Remove(exerciseCategoriesList.FirstOrDefault(x => x.Name == "Rzut wolny"));

                List<Exercise> randomExercises = new List<Exercise>();

                int atackSum = db.Exercises.Find(character.chosenAttacks.Exercise1).Level;
                atackSum += db.Exercises.Find(character.chosenAttacks.Exercise2).Level;
                atackSum += db.Exercises.Find(character.chosenAttacks.Exercise3).Level;
                List<int> pastNumbers = new List<int>();
                Random r = new Random();


                for (int i = 0; i < 3; i++)
                {
                    int temp;
                    if (atackSum > 5) //TODO level 10
                        temp = 5;
                    else if (atackSum > 0)
                        temp = atackSum;
                    else
                        temp = 1;

                    int rInt = r.Next(0, 10);
                    int rLevel = r.Next(1, temp);
                    atackSum -= rLevel;
                    while (pastNumbers.Contains(rInt))
                    {
                        rInt = r.Next(0, 10);
                    }
                    pastNumbers.Add(rInt);
                    var exerciseCategory = exerciseCategoriesList[rInt];
                    randomExercises.Add(db.Exercises.FirstOrDefault(x => x.ExerciseCategory.Name == exerciseCategory.Name && x.Level == rLevel));
                }

                int defenceSum = 0;

                foreach (var item in character.TraningRoom.TraningRoomByExercises)
                {
                    defenceSum += item.ExerciseDefence.Level;
                }

                List<Exercise> computerDefence = new List<Exercise>();

                foreach (var item in exerciseCategoriesList)
                {
                    int temp;
                    if (defenceSum > 5) //TODO level 10
                        temp = 5;
                    else if (defenceSum > 0)
                        temp = defenceSum;
                    else
                        temp = 1;

                    int rLevel = r.Next(1, temp);
                    defenceSum -= rLevel;

                    var tempExercise = db.Exercises.FirstOrDefault(x => x.ExerciseCategory.Name == item.Name && x.Level == rLevel);
                    computerDefence.Add(tempExercise);
                }


                int statSum = character.Speed + character.Strengh + character.Marksmanship + character.Defence;

                List<string> type = new List<string>();
                type.Add("computerSpeed");
                type.Add("computerStrengh");
                type.Add("computerMarksmanship");
                type.Add("computerDefenc");

                List<int> doneCategories = new List<int>();
                int computerSpeed = 0;
                int computerStrengh = 0;
                int computerMarksmanship = 0;
                int computerDefenc = 0;

                for (int i = 0; i < 3; i++)
                {
                    var rCategory = r.Next(0, 3);

                    while (doneCategories.Contains(rCategory))
                    {
                        rCategory = r.Next(0, 3);
                    }

                    switch (type[rCategory])
                    {
                        case "computerSpeed":
                            computerSpeed = r.Next(0, statSum);
                            statSum -= computerSpeed;
                            doneCategories.Add(rCategory);
                            break;
                        case "computerStrengh":
                            computerStrengh = r.Next(0, statSum);
                            statSum -= computerStrengh;
                            doneCategories.Add(rCategory);
                            break;
                        case "computerMarksmanship":
                            computerMarksmanship = r.Next(0, statSum);
                            statSum -= computerMarksmanship;
                            doneCategories.Add(rCategory);
                            break;
                        case "computerDefenc":
                            computerDefenc = r.Next(0, statSum);
                            statSum -= computerDefenc;
                            doneCategories.Add(rCategory);
                            break;
                    }
                }
                List<BattleHelper> atacksLog = new List<BattleHelper>();

                //1
                //player
                var compDef = computerDefence.FirstOrDefault(x => x.ExerciseCategory.Name == (db.Exercises.Find(character.chosenAttacks.Exercise1).ExerciseCategory.Name));
                var accurateThrow = (db.Exercises.Find(character.chosenAttacks.Exercise1).Level * 20) + character.Strengh + character.Defence + character.Speed + character.Marksmanship + 1200 - computerSpeed - computerDefenc - computerStrengh - computerMarksmanship - (compDef.Level * 20);
                accurateThrow /= 22;
                bool isSuccess = Rand(accurateThrow);
                if (!isSuccess)
                    if (character.Health > 10)
                        character.Health -= 10;
                    else
                        character.Health = 0;
                atacksLog.Add(new BattleHelper { Atack = compDef.ExerciseCategory.Name, Nick = user.UserData.Nick, isSuccess = isSuccess, NickOponent = "Komputer" });

                //PC
                var pcAtak = randomExercises[0];
                var exercises = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == pcAtak.ExerciseCategory.Name);
                int exerciseLevel = exercises.ExerciseDefence.Level;
                if (!exercises.IsCompleteDefence)
                    exerciseLevel--;


                accurateThrow = pcAtak.Level * 20 + computerStrengh + computerDefenc + computerSpeed + computerMarksmanship + 1200 - character.Strengh - character.Defence - character.Speed - character.Marksmanship - exerciseLevel * 20;
                accurateThrow /= 22;
                isSuccess = Rand(accurateThrow);
                atacksLog.Add(new BattleHelper { Atack = pcAtak.ExerciseCategory.Name, Nick = "Komputer", isSuccess = isSuccess, NickOponent = user.UserData.Nick });

                //2
                //player
                compDef = computerDefence.FirstOrDefault(x => x.ExerciseCategory.Name == (db.Exercises.Find(character.chosenAttacks.Exercise2).ExerciseCategory.Name));
                accurateThrow = (db.Exercises.Find(character.chosenAttacks.Exercise2).Level * 20) + character.Strengh + character.Defence + character.Speed + character.Marksmanship + 1200 - computerSpeed - computerDefenc - computerStrengh - computerMarksmanship - (compDef.Level * 20);
                accurateThrow /= 22;
                isSuccess = Rand(accurateThrow);
                if (!isSuccess)
                    if (character.Health > 10)
                        character.Health -= 10;
                    else
                        character.Health = 0;
                atacksLog.Add(new BattleHelper { Atack = compDef.ExerciseCategory.Name, Nick = user.UserData.Nick, isSuccess = isSuccess, NickOponent = "Komputer" });

                //PC
                pcAtak = randomExercises[1];
                exercises = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == pcAtak.ExerciseCategory.Name);
                exerciseLevel = exercises.ExerciseDefence.Level;
                if (!exercises.IsCompleteDefence)
                    exerciseLevel--;


                accurateThrow = pcAtak.Level * 20 + computerStrengh + computerDefenc + computerSpeed + computerMarksmanship + 1200 - character.Strengh - character.Defence - character.Speed - character.Marksmanship - exerciseLevel * 20;
                accurateThrow /= 22;
                isSuccess = Rand(accurateThrow);
                atacksLog.Add(new BattleHelper { Atack = pcAtak.ExerciseCategory.Name, Nick = "Komputer", isSuccess = isSuccess, NickOponent = user.UserData.Nick });

                //3
                //player
                compDef = computerDefence.FirstOrDefault(x => x.ExerciseCategory.Name == (db.Exercises.Find(character.chosenAttacks.Exercise3).ExerciseCategory.Name));
                accurateThrow = (db.Exercises.Find(character.chosenAttacks.Exercise3).Level * 20) + character.Strengh + character.Defence + character.Speed + character.Marksmanship + 1200 - computerSpeed - computerDefenc - computerStrengh - computerMarksmanship - (compDef.Level * 20);
                accurateThrow /= 22;
                isSuccess = Rand(accurateThrow);
                if (!isSuccess)
                    if (character.Health > 10)
                        character.Health -= 10;
                    else
                        character.Health = 0;
                atacksLog.Add(new BattleHelper { Atack = compDef.ExerciseCategory.Name, Nick = user.UserData.Nick, isSuccess = isSuccess, NickOponent = "Komputer" });

                //PC
                pcAtak = randomExercises[2];
                exercises = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == pcAtak.ExerciseCategory.Name);
                exerciseLevel = exercises.ExerciseDefence.Level;
                if (!exercises.IsCompleteDefence)
                    exerciseLevel--;


                accurateThrow = pcAtak.Level * 20 + computerStrengh + computerDefenc + computerSpeed + computerMarksmanship + 1200 - character.Strengh - character.Defence - character.Speed - character.Marksmanship - exerciseLevel * 20;
                accurateThrow /= 22;
                isSuccess = Rand(accurateThrow);
                atacksLog.Add(new BattleHelper { Atack = pcAtak.ExerciseCategory.Name, Nick = "Komputer", isSuccess = isSuccess, NickOponent = user.UserData.Nick });


                db.SaveChanges();
                return atacksLog;
            }
        }

        public void CheckChosenAttacks(ref BattleViewModel battleVM)
        {
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);

                if (character.chosenAttacks == null)
                    battleVM.isAttacksChosen = false;
                else
                {
                    if (character.chosenAttacks.Exercise1 != null &&
                        character.chosenAttacks.Exercise2 != null &&
                        character.chosenAttacks.Exercise3 != null)
                        battleVM.isAttacksChosen = true;
                    else
                        battleVM.isAttacksChosen = false;
                }
            }
        }

        public void AddChoseAttacksToCharacter(GameSettingsViewModel vm)
        {
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);

                if (character.chosenAttacks != null)
                {
                    character.chosenAttacks.Exercise1 = new Guid(vm.Exercise1);
                    character.chosenAttacks.Exercise2 = new Guid(vm.Exercise2);
                    character.chosenAttacks.Exercise3 = new Guid(vm.Exercise3);
                }
                else
                {
                    ChosenAttacks chosenAttacks = new ChosenAttacks();
                    chosenAttacks.Exercise1 = new Guid(vm.Exercise1);
                    chosenAttacks.Exercise2 = new Guid(vm.Exercise2);
                    chosenAttacks.Exercise3 = new Guid(vm.Exercise3);

                    character.chosenAttacks = chosenAttacks;
                }


                db.SaveChanges();
            }
        }

        public List<BattleLog> GenerateBattleText(List<BattleHelper> listOfAttacks)
        {
            List<BattleLog> battleLog = new List<BattleLog>();
            List<string> threeElement = new List<string>();
            List<string> blockElement = new List<string>();
            List<string> negativeElement = new List<string>();
            Random r = new Random();
            threeElement.Add("wykonuje blok");
            threeElement.Add("biegnie przeszkadzać w rzucie");
            threeElement.Add("próbuje blokować");

            blockElement.Add(" po nieudanym bloku {0}");
            blockElement.Add(",block {0} nieudany");

            negativeElement.Add("niecelny rzut");
            negativeElement.Add("{0} przeszkadza w rzucie, {0} przejmuje piłkę");

            int point = 0;
            int pointOponent = 0;
            int i = 0;

            string pointString = string.Empty;

            foreach (var item in listOfAttacks)
            {
                if (item.isAdditionalAtack)
                {
                    i++;

                    if (item.additionalAtackResult == 2)
                    {
                        string text = string.Format("{0} fauluje - {1} wykonuje rzuty wolne i zdobywa {2} punkty!", item.NickOponent, item.Nick, item.additionalAtackResult);
                        if (i % 2 == 0)
                            point += 2;
                        else
                            pointOponent += 2;
                        battleLog.Add(new BattleLog { leftSum = point.ToString(), rightSum = pointOponent.ToString(), text = text });
                    }
                    else if (item.additionalAtackResult == 1)
                    {
                        string text = string.Format("{0} fauluje - {1} wykonuje rzuty wolne i zdobywa {2} punkt!", item.NickOponent, item.Nick, item.additionalAtackResult);
                        if (i % 2 == 0)
                            point += 1;
                        else
                            pointOponent += 1;
                        battleLog.Add(new BattleLog { leftSum = point.ToString(), rightSum = pointOponent.ToString(), text = text });
                    }
                    else
                    {
                        string text = string.Format("{0} fauluje - {1} wykonuje rzuty wolne ale nie zdobywa żadnych punktów", item.NickOponent, item.Nick);

                        battleLog.Add(new BattleLog { leftSum = point.ToString(), rightSum = pointOponent.ToString(), text = text });
                    }
                }
                else
                {
                    i++;
                    switch (item.Atack)
                    {
                        case "Rzut za 3 punkty":
                            if (item.isSuccess)
                                if (i % 2 == 0)
                                    pointOponent += 3;
                                else
                                    point += 3;
                            pointString = "3 punkty";
                            break;
                        default:
                            if (item.isSuccess)
                                if (i % 2 == 0)
                                    pointOponent += 2;
                                else
                                    point += 2;
                            pointString = "2 punkty";
                            break;
                    }
                    int rInt = r.Next(0, 2);
                    int rIntBlock = r.Next(0, 1);
                    var temp = blockElement[rIntBlock].Replace("{0}", item.NickOponent);
                    string text = string.Format("{0} wykonuje {1}, {2} {3}", item.Nick, item.Atack, item.NickOponent, threeElement[rInt]);
                    if (item.isSuccess)
                    {
                        text = string.Format(text + " - {0} zdobył {1}{2}", item.Nick, pointString, temp);
                    }
                    else
                    {
                        rIntBlock = r.Next(0, 1);
                        temp = negativeElement[rIntBlock].Replace("{0}", item.NickOponent);
                        text = string.Format(text + " - {0} nie zdobył punktów {1}", item.Nick, temp);
                    }

                    battleLog.Add(new BattleLog { leftSum = point.ToString(), rightSum = pointOponent.ToString(), text = text });
                }
            }

            return battleLog;
        }

        public void ChangeStyleValue(string value)
        {
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);

                character.gameStyle = int.Parse(value);
                db.SaveChanges();
            }
        }


        public bool Rand(int chanse)
        {
            var randNumber = r.Next(1, 100);
            if (randNumber > (100 - chanse))
            {
                return true;
            }

            return false;
        }

    }
}