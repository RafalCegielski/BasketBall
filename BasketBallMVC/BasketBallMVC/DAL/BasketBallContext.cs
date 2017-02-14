
using BasketBallMVC.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BasketBallMVC.DAL
{
    public class BasketBallContext : IdentityDbContext<ApplicationUser>
    {
        public BasketBallContext() : base ("BasketBallContext")
        {
               
        }

        public static BasketBallContext Create()
        {
            return new BasketBallContext();
        }


        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceCategory> DeviceCategories { get; set; }
        public DbSet<ShopCategory> ShopCategories { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<TraningRoom> TrainingRooms { get; set; }
        public DbSet<TraningRoomByDevice> TraningRoomByDevices { get; set; }
        public DbSet<FriendInvitation> FriendsInvitation { get; set; }
        public DbSet<FriendList> FriendsList { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<TraningRoomByExercise> TraningRoomByExercises { get; set; }
        public DbSet<ExerciseCategory> ExerciseCategories { get; set; }
        public DbSet<ChosenAttacks> ChosenAttacks { get; set; }
        
    }
}