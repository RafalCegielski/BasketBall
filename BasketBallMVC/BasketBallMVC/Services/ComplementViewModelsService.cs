using BasketBallMVC.DAL;
using BasketBallMVC.Model;
using BasketBallMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketBallMVC.Services
{
    public class ComplementViewModelsService
    {
        ComplementLayoutViewModelService complementLayoutVM = new ComplementLayoutViewModelService();
        private string _identityName = HttpContext.Current.User.Identity.Name;

        public NotificationListViewModel ComplementNotificationListVM()
        {
            NotificationListViewModel notificationListVM = new NotificationListViewModel();
            List<Notification> notificationList = new List<Notification>();
            notificationListVM = complementLayoutVM.FillLayoutViewModel<NotificationListViewModel>();

            using (var db = new BasketBallContext())
            {
                var notifications = db.Notifications.Where(x => x.user.Email == _identityName).ToList();
                foreach (var item in notifications)
                {
                    notificationList.Add(new Notification { NotificationId = item.NotificationId, isOpen = item.isOpen, notificationDetails = item.notificationDetails, notificationType = item.notificationType });
                }
                notificationListVM.NotificationList = notificationList;
            }

            return notificationListVM;
        }

        public BattleViewModel ComplementBattleVM()
        {
            BattleViewModel battleVM = new BattleViewModel();
            battleVM = complementLayoutVM.FillLayoutViewModel<BattleViewModel>();

            return battleVM;
        }

        public TrainingRoomViewModel ComplementTrainingRoomVM()
        {
            TrainingRoomViewModel trainingRoomVM = new TrainingRoomViewModel();
            trainingRoomVM = complementLayoutVM.FillLayoutViewModel<TrainingRoomViewModel>();
            trainingRoomVM.Category = new List<Categories>();

            trainingRoomVM.Category.Add(new Categories { CategoryName = "Mały dystans", CategoryDistance = 1 });
            trainingRoomVM.Category.Add(new Categories { CategoryName = "Średni dystans", CategoryDistance = 2 });
            trainingRoomVM.Category.Add(new Categories { CategoryName = "Duży dystans", CategoryDistance = 3 });

            return trainingRoomVM;
        }

        public MessageListViewModel ComplementMessageListVM()
        {
            MessageListViewModel messageListVM = new MessageListViewModel();
            messageListVM = complementLayoutVM.FillLayoutViewModel<MessageListViewModel>();
            List<Message> messages = new List<Message>();
            List<MessageVM> messageVM = new List<MessageVM>();
            using (var db = new BasketBallContext())
            {
                if (db.FriendsList.Any(x => x.InvitedUserEmail == _identityName || x.InvitingUserEmail == _identityName))
                    messageListVM.HasFriends = true;
                else
                    messageListVM.HasFriends = false;

                messages = db.Messages.Where(x => x.Addressee.Email == _identityName).ToList();
                foreach (var item in messages)
                {
                    var adminRole = db.Roles.FirstOrDefault(x => x.Name == "Admin");
                    messageVM.Add(new MessageVM
                    {
                        Title = item.Title,
                        Details = item.Details,
                        isRead = item.isRead,
                        MessageId = item.MessageId,
                        SendDate = item.SendDate,
                        Sender = item.Sender.UserData.Nick
                    });
                }
                messageVM = messageVM.OrderByDescending(x => x.SendDate).ToList();
            }

            messageListVM.MessageList = messageVM;

            return messageListVM;
        }

        public GameSettingsViewModel ComplementGameSettingsVM()
        {
            GameSettingsViewModel gameSettingsVM = new GameSettingsViewModel();
            gameSettingsVM = complementLayoutVM.FillLayoutViewModel<GameSettingsViewModel>();
            using (var db = new BasketBallContext())
            {
                var loggedInUser = db.Users.FirstOrDefault(x => x.Email == _identityName);
                var character = db.Characters.FirstOrDefault(x => x.UserId == loggedInUser.Id);

                if (character.chosenAttacks != null && character.chosenAttacks.Exercise1 != null &&
                     character.chosenAttacks.Exercise2 != null && character.chosenAttacks.Exercise3 != null)
                {
                    gameSettingsVM.Exercise1 = character.chosenAttacks.Exercise1.ToString();
                    gameSettingsVM.Exercise2 = character.chosenAttacks.Exercise2.ToString();
                    gameSettingsVM.Exercise3 = character.chosenAttacks.Exercise3.ToString();
                }

            }

            return gameSettingsVM;
        }

        public ShopViewModel ComplementShopVM()
        {
            ShopViewModel shopVM = new ShopViewModel();
            shopVM = complementLayoutVM.FillLayoutViewModel<ShopViewModel>();

            using (var db = new BasketBallContext())
            {
                var shopCategories = db.ShopCategories.ToList();
                List<string> NamesOfCategories = new List<string>();

                foreach (var item in shopCategories)
                {
                    NamesOfCategories.Add(item.Name);
                }
                shopVM.NameOfCategory = NamesOfCategories;
            }

            return shopVM;
        }

        public HomeViewModel ComplementHomeVM()
        {
            HomeViewModel homeVM = new HomeViewModel();
            homeVM = complementLayoutVM.FillLayoutViewModel<HomeViewModel>();

            using (var db = new BasketBallContext())
            {
                #region last3Devices&Exercises
                var loggedInUser = db.Users.FirstOrDefault(x => x.Email == _identityName);
                var character = db.Characters.FirstOrDefault(x => x.UserId == loggedInUser.Id);

                var currenCharacterTraningRoomDevices = db.TrainingRooms.FirstOrDefault(x => x.Character.CharacterID == character.CharacterID).TraningRoomByDevices.ToList();
                var currenCharacterTraningRoomExercises = db.TrainingRooms.FirstOrDefault(x => x.Character.CharacterID == character.CharacterID).TraningRoomByExercises.ToList();

                if (currenCharacterTraningRoomDevices != null)
                {
                    var newDevices = currenCharacterTraningRoomDevices.OrderByDescending(a => a.BuyDate).Take(3).ToList();
                    List<Device> deviceList = new List<Device>();
                    if (newDevices != null)
                    {
                        foreach (var item in newDevices)
                        {
                            deviceList.Add(item.Device);
                        }
                        homeVM.RecentlyPurchasedDevices = deviceList;
                    }
                }
                if (currenCharacterTraningRoomExercises != null)
                {
                    List<TraningRoomByExercise> exercises = new List<TraningRoomByExercise>();

                    exercises = currenCharacterTraningRoomExercises.ToList();

                    List<DateTime> dateTimeList = new List<DateTime>();
                    foreach (var item in exercises)
                    {
                        if (item.AtackTrainingFinish != null && item.AtackTrainingFinish > new DateTime(1990, 01, 01) && item.IsCompleteAtack && item.ExerciseAtack != null  && item.ExerciseAtack.Level >= 1)
                        {
                            dateTimeList.Add(item.AtackTrainingFinish);
                        }
                        if (item.DefenceTrainingFinish != null && item.DefenceTrainingFinish > new DateTime(1990, 01, 01) && item.IsCompleteDefence && item.ExerciseAtack != null && item.ExerciseDefence.Level >= 1)
                        {
                            dateTimeList.Add(item.DefenceTrainingFinish);
                        }
                    }
                    dateTimeList = dateTimeList.OrderByDescending(x => x.Ticks).Take(3).ToList();

                    List<ExerciseViewModel> newExercisesVM = new List<ExerciseViewModel>();
                    foreach (var item in dateTimeList)
                    {
                        var searchItem = exercises.FirstOrDefault(x => x.AtackTrainingFinish == item);
                        if (searchItem != null)
                        {
                            newExercisesVM.Add(new ExerciseViewModel
                            {
                                Cost = searchItem.ExerciseAtack.Cost,
                                Energy = searchItem.ExerciseAtack.Energy,
                                ExerciseCategory = searchItem.ExerciseAtack.ExerciseCategory,
                                ExerciseID = searchItem.ExerciseAtack.ExerciseID,
                                Experience = searchItem.ExerciseAtack.Experience,
                                Level = searchItem.ExerciseAtack.Level,
                                Time = searchItem.ExerciseAtack.Time,
                                TraningRoomByExercises = searchItem.ExerciseAtack.TraningRoomByExercises,
                                Value = searchItem.ExerciseAtack.Value,
                                isAtack = true
                            });
                        }
                        else
                        {
                            searchItem = exercises.FirstOrDefault(x => x.DefenceTrainingFinish == item);
                            newExercisesVM.Add(new ExerciseViewModel
                            {
                                Cost = searchItem.ExerciseDefence.Cost,
                                Energy = searchItem.ExerciseDefence.Energy,
                                ExerciseCategory = searchItem.ExerciseDefence.ExerciseCategory,
                                ExerciseID = searchItem.ExerciseDefence.ExerciseID,
                                Experience = searchItem.ExerciseDefence.Experience,
                                Level = searchItem.ExerciseDefence.Level,
                                Time = searchItem.ExerciseDefence.Time,
                                TraningRoomByExercises = searchItem.ExerciseDefence.TraningRoomByExercises,
                                Value = searchItem.ExerciseDefence.Value,
                                isAtack = false
                            });
                        }
                    }

                    if (newExercisesVM != null && newExercisesVM.Count > 0 && newExercisesVM.FirstOrDefault().ExerciseCategory != null)
                        homeVM.RecentlyTrainedExercises = newExercisesVM;
                    else
                        homeVM.RecentlyTrainedExercises = new List<ExerciseViewModel>();
                }
                #endregion
                homeVM.Strengh = character.Strengh;
                homeVM.Marksmanship = character.Marksmanship;
                homeVM.Speed = character.Speed;
                homeVM.Defence = character.Defence;
                homeVM.WinGames = character.WinGames;
                homeVM.LoseGames = character.LoseGames;
                homeVM.GameCounter = character.GameCounter;
                homeVM.RankingPosition = character.RankingPosition;

            }

            return homeVM;
        }

        public RankingViewModel ComplementRankingVM()
        {
            RankingViewModel rankingVM = new RankingViewModel();
            rankingVM = complementLayoutVM.FillLayoutViewModel<RankingViewModel>();

            using (var db = new BasketBallContext())
            {
                var currentUser = db.Users.FirstOrDefault(x => x.Email == _identityName);
                var currentCharacter = db.Characters.FirstOrDefault(x => x.UserId == currentUser.Id);
                var allCharacters = db.Characters.ToList();
                var allUsers = db.Users.ToList();
                List<CharacterForRanking> CharacterForRanking = new List<ViewModel.CharacterForRanking>();

                foreach (var item in allCharacters)
                {
                    var user = allUsers.FirstOrDefault(x => x.Id == item.UserId);
                    CharacterForRanking.Add(new CharacterForRanking { Nick = user.UserData.Nick, TotalPoints = (item.Experience * 1 + item.WinGames * 3), PositionInRanking = item.RankingPosition, isAvalibleToBattle = (item.Level < (currentCharacter.Level + 3) && item.Level > (currentCharacter.Level - 3) && item.chosenAttacks != null && item.chosenAttacks.Exercise1 != null && item.chosenAttacks.Exercise2 != null && item.chosenAttacks.Exercise3 != null) ? true : false });
                }
                rankingVM.CharacterForRanking = CharacterForRanking;
            }

            return rankingVM;
        }

        public FriendsListViewModel ComplementFriendsListVM()
        {
            FriendsListViewModel friendsListVM = new FriendsListViewModel();
            friendsListVM = complementLayoutVM.FillLayoutViewModel<FriendsListViewModel>();
            List<FriendList> friendList = new List<FriendList>();
            List<CharacterForFriendList> characterForFriendList = new List<CharacterForFriendList>();


            using (var db = new BasketBallContext())
            {
                var currentUser = db.Users.FirstOrDefault(x => x.Email == _identityName);
                var currentCharacter = db.Characters.FirstOrDefault(x => x.UserId == currentUser.Id);
                friendList = db.FriendsList.Where(x => x.InvitedUserEmail == _identityName || x.InvitingUserEmail == _identityName).ToList();

                if (friendList != null)
                {
                    foreach (var item in friendList)
                    {
                        if (item.InvitedUserEmail == _identityName)
                        {
                            var user = db.Users.FirstOrDefault(x => x.Email == item.InvitingUserEmail);
                            var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);
                            characterForFriendList.Add(new CharacterForFriendList { Email = item.InvitingUserEmail, Nick = user.UserData.Nick, isAvalibleToAttack = ((character.Level < currentCharacter.Level + 3) && (character.Level > currentCharacter.Level - 3) && character.chosenAttacks!= null && character.chosenAttacks.Exercise1 != null && character.chosenAttacks.Exercise2 != null && character.chosenAttacks.Exercise3 != null) ? true : false });
                        }
                        if (item.InvitingUserEmail == _identityName)
                        {
                            var user = db.Users.FirstOrDefault(x => x.Email == item.InvitedUserEmail);
                            var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);
                            characterForFriendList.Add(new CharacterForFriendList { Email = item.InvitedUserEmail, Nick = user.UserData.Nick, isAvalibleToAttack = ((character.Level < currentCharacter.Level + 3) && (character.Level > currentCharacter.Level - 3) && character.chosenAttacks != null && character.chosenAttacks.Exercise1 != null && character.chosenAttacks.Exercise2 != null && character.chosenAttacks.Exercise3 != null) ? true : false });
                        }
                    }
                }
            }
            friendsListVM.CharacterForFriendList = characterForFriendList;

            return friendsListVM;
        }

        public CharacterProfilViewModel ComplementCharacterProfilVM(string UserId)
        {
            CharacterProfilViewModel characterProfilVM = new CharacterProfilViewModel();
            characterProfilVM = complementLayoutVM.FillLayoutViewModel<CharacterProfilViewModel>();

            using (var db = new BasketBallContext())
            {
                #region last3Devices&Exercises
                var user = db.Users.Find(UserId);
                var character = db.Characters.FirstOrDefault(x => x.UserId == UserId);

                var currenCharacterTraningRoomDevices = db.TrainingRooms.FirstOrDefault(x => x.Character.CharacterID == character.CharacterID).TraningRoomByDevices.ToList();
                var currenCharacterTraningRoomExercises = db.TrainingRooms.FirstOrDefault(x => x.Character.CharacterID == character.CharacterID).TraningRoomByExercises.ToList();

                if (currenCharacterTraningRoomDevices != null)
                {
                    var newDevices = currenCharacterTraningRoomDevices.OrderByDescending(a => a.BuyDate).Take(3).ToList();
                    List<Device> deviceList = new List<Device>();
                    if (newDevices != null)
                    {
                        foreach (var item in newDevices)
                        {
                            deviceList.Add(item.Device);
                        }
                        characterProfilVM.RecentlyPurchasedDevices = deviceList;
                    }
                }
                if (currenCharacterTraningRoomExercises != null)
                {
                    List<TraningRoomByExercise> exercises = new List<TraningRoomByExercise>();

                    exercises = currenCharacterTraningRoomExercises.ToList();

                    List<DateTime> dateTimeList = new List<DateTime>();
                    foreach (var item in exercises)
                    {
                        if (item.AtackTrainingFinish != null && item.AtackTrainingFinish > new DateTime(1990, 01, 01) && item.IsCompleteAtack && item.ExerciseAtack.Level > 0)
                        {
                            dateTimeList.Add(item.AtackTrainingFinish);
                        }
                        if (item.DefenceTrainingFinish != null && item.DefenceTrainingFinish > new DateTime(1990, 01, 01) && item.IsCompleteDefence && item.ExerciseDefence.Level > 0)
                        {
                            dateTimeList.Add(item.DefenceTrainingFinish);
                        }
                    }
                    dateTimeList = dateTimeList.OrderByDescending(x => x.Ticks).Take(3).ToList();

                    List<ExerciseViewModel> newExercisesVM = new List<ExerciseViewModel>();
                    foreach (var item in dateTimeList)
                    {
                        var searchItem = exercises.FirstOrDefault(x => x.AtackTrainingFinish == item);
                        if (searchItem != null)
                        {
                            newExercisesVM.Add(new ExerciseViewModel
                            {
                                Cost = searchItem.ExerciseAtack.Cost,
                                Energy = searchItem.ExerciseAtack.Energy,
                                ExerciseCategory = searchItem.ExerciseAtack.ExerciseCategory,
                                ExerciseID = searchItem.ExerciseAtack.ExerciseID,
                                Experience = searchItem.ExerciseAtack.Experience,
                                Level = searchItem.ExerciseAtack.Level,
                                Time = searchItem.ExerciseAtack.Time,
                                TraningRoomByExercises = searchItem.ExerciseAtack.TraningRoomByExercises,
                                Value = searchItem.ExerciseAtack.Value,
                                isAtack = true
                            });
                        }
                        else
                        {
                            searchItem = exercises.FirstOrDefault(x => x.DefenceTrainingFinish == item);
                            newExercisesVM.Add(new ExerciseViewModel
                            {
                                Cost = searchItem.ExerciseDefence.Cost,
                                Energy = searchItem.ExerciseDefence.Energy,
                                ExerciseCategory = searchItem.ExerciseDefence.ExerciseCategory,
                                ExerciseID = searchItem.ExerciseDefence.ExerciseID,
                                Experience = searchItem.ExerciseDefence.Experience,
                                Level = searchItem.ExerciseDefence.Level,
                                Time = searchItem.ExerciseDefence.Time,
                                TraningRoomByExercises = searchItem.ExerciseDefence.TraningRoomByExercises,
                                Value = searchItem.ExerciseDefence.Value,
                                isAtack = false
                            });
                        }
                    }

                    if (newExercisesVM != null && newExercisesVM.FirstOrDefault() != null && newExercisesVM.FirstOrDefault().ExerciseCategory != null)
                        characterProfilVM.RecentlyTrainedExercises = newExercisesVM;
                    else
                        characterProfilVM.RecentlyTrainedExercises = new List<ExerciseViewModel>();
                }
                #endregion
                characterProfilVM.Strengh = character.Strengh;
                characterProfilVM.Marksmanship = character.Marksmanship;
                characterProfilVM.Speed = character.Speed;
                characterProfilVM.Defence = character.Defence;
                characterProfilVM.WinGames = character.WinGames;
                characterProfilVM.LoseGames = character.LoseGames;
                characterProfilVM.GameCounter = character.GameCounter;
                characterProfilVM.RankingPosition = character.RankingPosition;
                characterProfilVM.TargetUserName = user.UserData.Nick;
            }

            return characterProfilVM;
        }

        public RestaurantViewModel ComplementRestaurantVM()
        {
            RestaurantViewModel restaurantVM = new RestaurantViewModel();
            restaurantVM = complementLayoutVM.FillLayoutViewModel<RestaurantViewModel>();

            int smallCost = 50 + restaurantVM.Level * 10;
            int mediumCost = 60 + restaurantVM.Level * 20;
            int bigCost = 100 + restaurantVM.Level * 30;

            restaurantVM.SmallCost = smallCost;
            restaurantVM.MediumCost = mediumCost;
            restaurantVM.BigCost = bigCost;
            restaurantVM.isSmallAvailible = (smallCost <= restaurantVM.Gold) ? true : false;
            restaurantVM.isMediumAvailible = (mediumCost <= restaurantVM.Gold) ? true : false;
            restaurantVM.isBigAvailible = (bigCost <= restaurantVM.Gold) ? true : false;

            return restaurantVM;
        }

        public AdminProfileViewModel ComplementAdminProfileVM(string userId)
        {
            AdminProfileViewModel adminProfileVM;
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Id == userId);
                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);

                adminProfileVM = new AdminProfileViewModel
                {
                    Energy = character.Energy,
                    Gold = character.Gold,
                    Health = character.Health,
                    TargetUserName = user.UserData.Nick
                };
            }
            return adminProfileVM;
        }

        public void UpdateBattleVM(ref BattleViewModel battleVM)
        {
            LayoutViewModel layoutVM = new LayoutViewModel();
            layoutVM = complementLayoutVM.FillLayoutViewModel<LayoutViewModel>();
        }

        public GameStyleViewModel ComplementGameStyleVM()
        {
            using (var db = new BasketBallContext())
            {
                var gameStyleVM = complementLayoutVM.FillLayoutViewModel<GameStyleViewModel>();
                var currentUser = db.Users.FirstOrDefault(x => x.Email == _identityName);
                gameStyleVM.GameStyle = db.Characters.FirstOrDefault(x => x.UserId == currentUser.Id).gameStyle;

                return gameStyleVM;
            }
        }
    }
}