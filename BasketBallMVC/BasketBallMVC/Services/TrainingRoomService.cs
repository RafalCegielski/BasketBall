using BasketBallMVC.DAL;
using BasketBallMVC.Model;
using BasketBallMVC.ViewModel;
using Hangfire;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasketBallMVC.Services
{
    public class TrainingRoomService
    {
        public void CancelTraining(string btnId, string exerciseName)
        {
            btnId = btnId.Replace("cancel", "");
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);
                var target = db.TrainingRooms.FirstOrDefault(x => x.TraningRoomID == character.TraningRoom.TraningRoomID).TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == exerciseName);
                var exercises = db.Exercises.Where(x => x.ExerciseCategory.Name == exerciseName);
                BackgroundJob.Delete(target.JobId);

                if (btnId == "Atack")
                {
                    target.IsCompleteAtack = true;
                    int level = target.ExerciseAtack.Level;
                    if (level > 0)
                    {
                        level -= 1;
                    }
                    target.ExerciseAtack = exercises.FirstOrDefault(x => x.Level == level);
                }
                else
                {
                    target.IsCompleteDefence = true;
                    int level = target.ExerciseDefence.Level;
                    if (level > 0)
                    {
                        level -= 1;
                    }
                    target.ExerciseDefence = exercises.FirstOrDefault(x => x.Level == level);
                }
                character.IsBusy = false;
                character.BusyEndTime = DateTime.Now.AddMinutes(-1);
                db.SaveChanges();
            }
        }

        public SelectList CreateExerciseList(ref GameSettingsViewModel gameSettingsVM)
        {
            List<ExerciseList> exercises = new List<ExerciseList>();
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);

                var atacks = character.TraningRoom.TraningRoomByExercises.Where(x => x.ExerciseAtack.Level > 0).ToList();
                if (atacks.Count >= 3)
                {
                    for (int j = 0; j < atacks.Count; j++)
                    {
                        if (!atacks[j].IsCompleteAtack && atacks[j].ExerciseAtack.Level == 1)
                        {
                            atacks.Remove(atacks[j]);
                        }
                        else if (!atacks[j].IsCompleteAtack && atacks[j].ExerciseAtack.Level > 1)
                        {
                            var exercise = db.Exercises.FirstOrDefault(x => x.ExerciseCategory.Name == atacks[j].ExerciseAtack.ExerciseCategory.Name &&
                            x.Level == atacks[j].ExerciseAtack.Level - 1);
                            atacks[j].ExerciseAtack = exercise;
                        }
                    }
                    int i = 0;

                    for (int j = 0; j < atacks.Count; j++)
                    {
                        i++;
                        exercises.Add(new ExerciseList { Name = atacks[j].ExerciseAtack.ExerciseCategory.Name, ExerciseID = atacks[j].ExerciseAtack.ExerciseID });
                    }
                    if (i < 3)
                        gameSettingsVM.isThreeOrMoreExercises = false;
                    else
                        gameSettingsVM.isThreeOrMoreExercises = true;
                }

            }

            Dictionary<string, string> newDictionary = new Dictionary<string, string>();

            var selectList = new SelectList(exercises, "ExerciseID", "Name");

            return selectList;
        }

        public string GetExerciseData(string exerciseName)
        {
            TrainingRoomAjax trainingRoomAjax = new TrainingRoomAjax();
            string json = string.Empty;
            exerciseName = exerciseName.Replace("ExerciseCategoryButton_", "");
            trainingRoomAjax.ExerciseName = exerciseName;
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);
                var exerciseCategory = db.ExerciseCategories.FirstOrDefault(x => x.Name == exerciseName);
                if (exerciseCategory.Exercises != null)
                    exerciseCategory.Exercises = null;
                var temp = character.TraningRoom.TraningRoomByExercises.FirstOrDefault();

                var traingingRoomDefenceExercise = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseDefence.ExerciseCategory.Name == exerciseCategory.Name);
                if (traingingRoomDefenceExercise != null)
                {
                    trainingRoomAjax.isDefenceCurrentlyTrained = !traingingRoomDefenceExercise.IsCompleteDefence;
                    var exercise = traingingRoomDefenceExercise.ExerciseDefence;
                    exercise = db.Exercises.FirstOrDefault(x => x.ExerciseCategory.Name == exercise.ExerciseCategory.Name && x.Level == exercise.Level + 1);

                    trainingRoomAjax.Defence = exercise;
                }

                var traingingRoomAtackExercise = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseAtack.ExerciseCategory.Name == exerciseCategory.Name);
                if (traingingRoomAtackExercise != null)
                {
                    trainingRoomAjax.isAtackCurrentlyTrained = !traingingRoomAtackExercise.IsCompleteAtack;
                    var exercise = traingingRoomAtackExercise.ExerciseAtack;
                    exercise = db.Exercises.FirstOrDefault(x => x.ExerciseCategory.Name == exercise.ExerciseCategory.Name && x.Level == exercise.Level + 1);

                    trainingRoomAjax.Atack = exercise;
                }

                json = JsonConvert.SerializeObject(trainingRoomAjax);
            }

            return json;
        }


        public bool StartTraining(string btnId, string exerciseName)
        {
            btnId = btnId.Replace("practice", "");
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                var exerciseCategory = db.ExerciseCategories.FirstOrDefault(x => x.Name == exerciseName);
                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);
                Exercise targetExercise = new Exercise();
                if (btnId == "Atack")
                {
                    var currentExercise = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseDefence.ExerciseCategory.Name == exerciseCategory.Name);
                    int exerciseLevel;
                    if (currentExercise != null)
                    {
                        exerciseLevel = currentExercise.ExerciseAtack.Level + 1;
                        if (exerciseLevel > 20)
                        {
                            exerciseLevel = 20;
                        }
                    }
                    else
                    {
                        exerciseLevel = 1;
                    }
                    targetExercise = db.Exercises.FirstOrDefault(x => x.ExerciseCategory.Name == exerciseCategory.Name && x.Level == exerciseLevel);
                }
                else if (btnId == "Defence")
                {
                    var currentExercise = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseAtack.ExerciseCategory.Name == exerciseCategory.Name);
                    int exerciseLevel;
                    if (currentExercise != null)
                    {
                        exerciseLevel = currentExercise.ExerciseDefence.Level + 1;
                        if (exerciseLevel > 20)
                        {
                            exerciseLevel = 20;
                        }
                    }
                    else
                    {
                        exerciseLevel = 1;
                    }

                    targetExercise = db.Exercises.FirstOrDefault(x => x.ExerciseCategory.Name == exerciseCategory.Name && x.Level == exerciseLevel);
                }
                if (character.Gold >= targetExercise.Cost && character.Energy >= targetExercise.Energy)
                {
                    character.IsBusy = true;
                    character.BusyEndTime = DateTime.Now.AddMinutes(targetExercise.Time);
                    var existingExercise = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == targetExercise.ExerciseCategory.Name);
                    var jobId = BackgroundJob.Schedule(
                                () => UpdateCharacterData(character.CharacterID, targetExercise.ExerciseCategory.Name),
                                TimeSpan.FromMinutes(targetExercise.Time));
                    if (existingExercise != null)
                    {
                        if (btnId == "Defence")
                        {
                            existingExercise.ExerciseDefence = targetExercise;
                            existingExercise.IsCompleteDefence = false;
                            existingExercise.JobId = jobId;
                        }
                        else
                        {
                            existingExercise.ExerciseAtack = targetExercise;
                            existingExercise.IsCompleteAtack = false;
                            existingExercise.JobId = jobId;
                        }
                    }
                    else
                    {

                        if (btnId == "Defence")
                        {
                            character.TraningRoom.TraningRoomByExercises.Add(new TraningRoomByExercise { IsCompleteDefence = false, IsCompleteAtack = true, TraningRoom = character.TraningRoom, TraningRoomByExerciseID = Guid.NewGuid(), ExerciseDefence = targetExercise, JobId = jobId, ExerciseCategoryName = targetExercise.ExerciseCategory.Name });
                        }
                        else
                        {
                            character.TraningRoom.TraningRoomByExercises.Add(new TraningRoomByExercise { IsCompleteAtack = false, IsCompleteDefence = true, TraningRoom = character.TraningRoom, TraningRoomByExerciseID = Guid.NewGuid(), ExerciseAtack = targetExercise, JobId = jobId, ExerciseCategoryName = targetExercise.ExerciseCategory.Name });
                        }
                    }
                    var client = new BackgroundJobClient();

                    character.Gold -= targetExercise.Cost;
                    character.Energy -= targetExercise.Energy;

                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void UpdateCharacterData(Guid CharacterID, string exerciseCategoryName)
        {
            using (var db = new BasketBallContext())
            {
                var character = db.Characters.FirstOrDefault(x => x.CharacterID == CharacterID);
                var target = character.TraningRoom.TraningRoomByExercises.FirstOrDefault(x => x.ExerciseCategoryName == exerciseCategoryName);
                character.IsBusy = false;
                if (!target.IsCompleteAtack)
                {
                    target.IsCompleteAtack = true;
                    target.AtackTrainingFinish = DateTime.Now;
                }
                if (!target.IsCompleteDefence)
                {
                    target.IsCompleteDefence = true;
                    target.DefenceTrainingFinish = DateTime.Now;
                }
                db.SaveChanges();
            }
        }

        public string GetExerciseCategories(string distance)
        {
            int distanceInt = int.Parse(distance);
            string json = string.Empty;

            using (var db = new BasketBallContext())
            {
                List<ExerciseCategory> exerciseCategories = db.ExerciseCategories.Where(x => x.Distance == distanceInt).ToList();
                List<ExerciseCategory> exerciseCategoriesTemp = new List<ExerciseCategory>();
                foreach (var item in exerciseCategories)
                {
                    exerciseCategoriesTemp.Add(new ExerciseCategory { Distance = item.Distance, ExerciseCategoryId = item.ExerciseCategoryId, Name = item.Name });
                }

                json = JsonConvert.SerializeObject(exerciseCategoriesTemp);
            }
            return json;
        }

        public List<Device> GetAllOurDevices()
        {
            using (var db = new BasketBallContext())
            {
                var loggedInUser = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                var character = db.Characters.FirstOrDefault(x => x.UserId == loggedInUser.Id);
                var currenCharacterTraningRoom = db.TrainingRooms.FirstOrDefault(x => x.Character.CharacterID == character.CharacterID).TraningRoomByDevices.ToList();
                List<Device> deviceList = new List<Device>();

                foreach (var item in currenCharacterTraningRoom)
                {
                    deviceList.Add(item.Device);
                }

                return deviceList;
            }
        }
    }
}