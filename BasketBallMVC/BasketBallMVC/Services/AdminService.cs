using BasketBallMVC.DAL;
using BasketBallMVC.Hubs;
using BasketBallMVC.Model;
using BasketBallMVC.ViewModel;
using Hangfire;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketBallMVC.Services
{
    public class AdminService
    {

        public AdminUsersViewModel GetUserList()
        {
            AdminUsersViewModel users = new AdminUsersViewModel();
            users.pagedList = new List<AdminUserDetails>();
            using (var db = new BasketBallContext())
            {
                UserManager<ApplicationUser> userMenager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var idenityUsers = db.Users.ToList();
                foreach (var item in idenityUsers)
                {
                    if (!userMenager.IsInRole(item.Id, "Admin"))
                        users.pagedList.Add(new AdminUserDetails { email = item.Email, id = item.Id, name = item.UserData.Nick, isBanned = (item.LockoutEndDateUtc > DateTime.UtcNow) ? true : false });
                }
            }

            return users;
        }

        public void DeleteUserWithDependencies(string userId)
        {
            using (var db = new BasketBallContext())
            {
                UserManager<ApplicationUser> userMenager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = userMenager.FindById(userId);

                var friendInvitations = db.FriendsInvitation.Where(x => x.InvitedUserEmail == user.Email || x.InvitingUserEmail == user.Email).ToList();
                db.FriendsInvitation.RemoveRange(friendInvitations);

                var friendList = db.FriendsList.Where(x => x.InvitedUserEmail == user.Email || x.InvitedUserEmail == user.Email).ToList();
                db.FriendsList.RemoveRange(friendList);

                var messages = db.Messages.Where(x => x.Addressee.Id == user.Id || x.Sender.Id == user.Id).ToList();
                db.Messages.RemoveRange(messages);

                var chosenAtacks = db.ChosenAttacks.FirstOrDefault(x => x.character.UserId == user.Id);
                if (chosenAtacks != null)
                    db.ChosenAttacks.Remove(chosenAtacks);

                var trainingRoom = db.TrainingRooms.FirstOrDefault(x => x.Character.UserId == user.Id);
                if (trainingRoom != null)
                {
                    var trainingRoomByDevices = db.TraningRoomByDevices.Where(x => x.TraningRoom.TraningRoomID == trainingRoom.TraningRoomID).ToList();
                    var traingingRoomByExercises = db.TraningRoomByExercises.Where(x => x.TraningRoom.TraningRoomID == trainingRoom.TraningRoomID).ToList();

                    foreach (var item in traingingRoomByExercises)
                    {
                        if (!item.IsCompleteAtack || !item.IsCompleteDefence)
                            BackgroundJob.Delete(item.JobId);
                    }

                    db.TraningRoomByExercises.RemoveRange(traingingRoomByExercises);
                    db.TraningRoomByDevices.RemoveRange(trainingRoomByDevices);
                    db.TrainingRooms.Remove(trainingRoom);
                }

                var notifications = db.Notifications.Where(x => x.user.Id == user.Id).ToList();
                db.Notifications.RemoveRange(notifications);

                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);
                if (character != null)
                    db.Characters.Remove(character);

                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public void BanUser(string value, string userId)
        {
            using (var db = new BasketBallContext())
            {
                UserManager<ApplicationUser> userMenager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = userMenager.FindById(userId);
                NotificationHub.LogoutUser(user.Email);
                //NotificationHub.LogoutUser(user.Email);
                switch (value)
                {
                    case "1":
                        user.LockoutEndDateUtc = DateTime.UtcNow.AddHours(1);
                        break;
                    case "2":
                        user.LockoutEndDateUtc = DateTime.UtcNow.AddDays(1);
                        break;
                    case "3":
                        user.LockoutEndDateUtc = DateTime.UtcNow.AddMonths(1);
                        break;
                    case "4":
                        user.LockoutEndDateUtc = DateTime.UtcNow.AddYears(1);
                        break;
                    case "5":
                        user.LockoutEndDateUtc = DateTime.UtcNow.AddYears(10);
                        break;
                }
                db.SaveChanges();
            }
        }

        public void UnbanUser(string userId)
        {
            using (var db = new BasketBallContext())
            {
                UserManager<ApplicationUser> userMenager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = userMenager.FindById(userId);
                user.LockoutEndDateUtc = null;
                db.SaveChanges();
            }
        }

        public void ChangeCharacterData(AdminProfileViewModel vm)
        {
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.UserData.Nick == vm.TargetUserName);
                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);
                
                character.Energy = vm.Energy;
                character.Gold = vm.Gold;
                character.Health = vm.Health;

                db.SaveChanges();
            }
        }
    }
}