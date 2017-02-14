namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasketBallDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        CharacterID = c.Guid(nullable: false),
                        Strengh = c.Int(nullable: false),
                        Marksmanship = c.Int(nullable: false),
                        Defence = c.Int(nullable: false),
                        Experience = c.Int(nullable: false),
                        WinGames = c.Int(nullable: false),
                        LoseGames = c.Int(nullable: false),
                        GameCounter = c.Int(nullable: false),
                        UserId = c.String(),
                        Gold = c.Int(nullable: false),
                        Energy = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        Health = c.Int(nullable: false),
                        Speed = c.Int(nullable: false),
                        RankingPosition = c.Int(nullable: false),
                        IsBusy = c.Boolean(nullable: false),
                        BusyEndTime = c.DateTime(nullable: false),
                        gameStyle = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CharacterID);
            
            CreateTable(
                "dbo.ChosenAttacks",
                c => new
                    {
                        ChosenAttacksId = c.Guid(nullable: false),
                        Exercise1 = c.Guid(nullable: false),
                        Exercise2 = c.Guid(nullable: false),
                        Exercise3 = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ChosenAttacksId)
                .ForeignKey("dbo.Characters", t => t.ChosenAttacksId)
                .Index(t => t.ChosenAttacksId);
            
            CreateTable(
                "dbo.TraningRooms",
                c => new
                    {
                        TraningRoomID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.TraningRoomID)
                .ForeignKey("dbo.Characters", t => t.TraningRoomID)
                .Index(t => t.TraningRoomID);
            
            CreateTable(
                "dbo.TraningRoomByDevices",
                c => new
                    {
                        TraningRoomByDeviceID = c.Guid(nullable: false),
                        BuyDate = c.DateTime(nullable: false),
                        Device_DeviceID = c.Guid(),
                        TraningRoom_TraningRoomID = c.Guid(),
                    })
                .PrimaryKey(t => t.TraningRoomByDeviceID)
                .ForeignKey("dbo.Devices", t => t.Device_DeviceID)
                .ForeignKey("dbo.TraningRooms", t => t.TraningRoom_TraningRoomID)
                .Index(t => t.Device_DeviceID)
                .Index(t => t.TraningRoom_TraningRoomID);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        DeviceID = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        DeviceCategory_DeviceCategoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.DeviceID)
                .ForeignKey("dbo.DeviceCategories", t => t.DeviceCategory_DeviceCategoryId)
                .Index(t => t.DeviceCategory_DeviceCategoryId);
            
            CreateTable(
                "dbo.DeviceCategories",
                c => new
                    {
                        DeviceCategoryId = c.Guid(nullable: false),
                        Name = c.String(),
                        deviceMarker = c.Int(nullable: false),
                        ShopCategory_ShopCategoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.DeviceCategoryId)
                .ForeignKey("dbo.ShopCategories", t => t.ShopCategory_ShopCategoryId)
                .Index(t => t.ShopCategory_ShopCategoryId);
            
            CreateTable(
                "dbo.ShopCategories",
                c => new
                    {
                        ShopCategoryId = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ShopCategoryId);
            
            CreateTable(
                "dbo.TraningRoomByExercises",
                c => new
                    {
                        TraningRoomByExerciseID = c.Guid(nullable: false),
                        IsCompleteAtack = c.Boolean(nullable: false),
                        IsCompleteDefence = c.Boolean(nullable: false),
                        JobId = c.String(),
                        ExerciseCategoryName = c.String(),
                        AtackTrainingFinish = c.DateTime(nullable: false),
                        DefenceTrainingFinish = c.DateTime(nullable: false),
                        Exercise_ExerciseID = c.Guid(),
                        ExerciseAtack_ExerciseID = c.Guid(),
                        ExerciseDefence_ExerciseID = c.Guid(),
                        TraningRoom_TraningRoomID = c.Guid(),
                    })
                .PrimaryKey(t => t.TraningRoomByExerciseID)
                .ForeignKey("dbo.Exercises", t => t.Exercise_ExerciseID)
                .ForeignKey("dbo.Exercises", t => t.ExerciseAtack_ExerciseID)
                .ForeignKey("dbo.Exercises", t => t.ExerciseDefence_ExerciseID)
                .ForeignKey("dbo.TraningRooms", t => t.TraningRoom_TraningRoomID)
                .Index(t => t.Exercise_ExerciseID)
                .Index(t => t.ExerciseAtack_ExerciseID)
                .Index(t => t.ExerciseDefence_ExerciseID)
                .Index(t => t.TraningRoom_TraningRoomID);
            
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        ExerciseID = c.Guid(nullable: false),
                        Time = c.Int(nullable: false),
                        Experience = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        Cost = c.Int(nullable: false),
                        Energy = c.Int(nullable: false),
                        ExerciseCategory_ExerciseCategoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.ExerciseID)
                .ForeignKey("dbo.ExerciseCategories", t => t.ExerciseCategory_ExerciseCategoryId)
                .Index(t => t.ExerciseCategory_ExerciseCategoryId);
            
            CreateTable(
                "dbo.ExerciseCategories",
                c => new
                    {
                        ExerciseCategoryId = c.Guid(nullable: false),
                        Name = c.String(),
                        Distance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExerciseCategoryId);
            
            CreateTable(
                "dbo.FriendInvitations",
                c => new
                    {
                        FriendInvitationId = c.Guid(nullable: false),
                        InvitingUserEmail = c.String(),
                        InvitedUserEmail = c.String(),
                    })
                .PrimaryKey(t => t.FriendInvitationId);
            
            CreateTable(
                "dbo.FriendLists",
                c => new
                    {
                        FriendListID = c.Guid(nullable: false),
                        InvitingUserEmail = c.String(),
                        InvitedUserEmail = c.String(),
                    })
                .PrimaryKey(t => t.FriendListID);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        LevelId = c.Guid(nullable: false),
                        Lvl = c.Int(nullable: false),
                        MaxHealth = c.Int(nullable: false),
                        MaxEnergy = c.Int(nullable: false),
                        MaxExperience = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LevelId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Guid(nullable: false),
                        Title = c.String(),
                        Details = c.String(),
                        SendDate = c.DateTime(nullable: false),
                        isRead = c.Boolean(nullable: false),
                        Addressee_Id = c.String(maxLength: 128),
                        Sender_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.AspNetUsers", t => t.Addressee_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Sender_Id)
                .Index(t => t.Addressee_Id)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserData_FirstName = c.String(),
                        UserData_LastName = c.String(),
                        UserData_Nick = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.Guid(nullable: false),
                        notificationType = c.Int(nullable: false),
                        notificationDetails = c.String(),
                        isOpen = c.Boolean(nullable: false),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.NotificationId)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .Index(t => t.user_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Messages", "Sender_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "Addressee_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notifications", "user_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TraningRoomByExercises", "TraningRoom_TraningRoomID", "dbo.TraningRooms");
            DropForeignKey("dbo.TraningRoomByExercises", "ExerciseDefence_ExerciseID", "dbo.Exercises");
            DropForeignKey("dbo.TraningRoomByExercises", "ExerciseAtack_ExerciseID", "dbo.Exercises");
            DropForeignKey("dbo.TraningRoomByExercises", "Exercise_ExerciseID", "dbo.Exercises");
            DropForeignKey("dbo.Exercises", "ExerciseCategory_ExerciseCategoryId", "dbo.ExerciseCategories");
            DropForeignKey("dbo.TraningRoomByDevices", "TraningRoom_TraningRoomID", "dbo.TraningRooms");
            DropForeignKey("dbo.TraningRoomByDevices", "Device_DeviceID", "dbo.Devices");
            DropForeignKey("dbo.DeviceCategories", "ShopCategory_ShopCategoryId", "dbo.ShopCategories");
            DropForeignKey("dbo.Devices", "DeviceCategory_DeviceCategoryId", "dbo.DeviceCategories");
            DropForeignKey("dbo.TraningRooms", "TraningRoomID", "dbo.Characters");
            DropForeignKey("dbo.ChosenAttacks", "ChosenAttacksId", "dbo.Characters");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "user_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Messages", new[] { "Sender_Id" });
            DropIndex("dbo.Messages", new[] { "Addressee_Id" });
            DropIndex("dbo.Exercises", new[] { "ExerciseCategory_ExerciseCategoryId" });
            DropIndex("dbo.TraningRoomByExercises", new[] { "TraningRoom_TraningRoomID" });
            DropIndex("dbo.TraningRoomByExercises", new[] { "ExerciseDefence_ExerciseID" });
            DropIndex("dbo.TraningRoomByExercises", new[] { "ExerciseAtack_ExerciseID" });
            DropIndex("dbo.TraningRoomByExercises", new[] { "Exercise_ExerciseID" });
            DropIndex("dbo.DeviceCategories", new[] { "ShopCategory_ShopCategoryId" });
            DropIndex("dbo.Devices", new[] { "DeviceCategory_DeviceCategoryId" });
            DropIndex("dbo.TraningRoomByDevices", new[] { "TraningRoom_TraningRoomID" });
            DropIndex("dbo.TraningRoomByDevices", new[] { "Device_DeviceID" });
            DropIndex("dbo.TraningRooms", new[] { "TraningRoomID" });
            DropIndex("dbo.ChosenAttacks", new[] { "ChosenAttacksId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Notifications");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Messages");
            DropTable("dbo.Levels");
            DropTable("dbo.FriendLists");
            DropTable("dbo.FriendInvitations");
            DropTable("dbo.ExerciseCategories");
            DropTable("dbo.Exercises");
            DropTable("dbo.TraningRoomByExercises");
            DropTable("dbo.ShopCategories");
            DropTable("dbo.DeviceCategories");
            DropTable("dbo.Devices");
            DropTable("dbo.TraningRoomByDevices");
            DropTable("dbo.TraningRooms");
            DropTable("dbo.ChosenAttacks");
            DropTable("dbo.Characters");
        }
    }
}
