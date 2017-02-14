namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exerciseFix2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExerciseByDevices", "Device_DeviceID", "dbo.Devices");
            DropForeignKey("dbo.TraningRooms", "Character_CharacterID", "dbo.Characters");
            DropForeignKey("dbo.TraningRoomByDevices", "TraningRoom_TraningRoomID", "dbo.TraningRooms");
            DropForeignKey("dbo.TraningRoomByExercises", "TraningRoom_TraningRoomID", "dbo.TraningRooms");
            DropForeignKey("dbo.ExerciseByDevices", "ExerciseCategory_ExerciseCategoryId", "dbo.ExerciseCategories");
            DropIndex("dbo.ExerciseByDevices", new[] { "Device_DeviceID" });
            DropIndex("dbo.ExerciseByDevices", new[] { "ExerciseCategory_ExerciseCategoryId" });
            DropIndex("dbo.TraningRoomByExercises", new[] { "TraningRoom_TraningRoomID" });
            DropIndex("dbo.TraningRooms", new[] { "Character_CharacterID" });
            DropIndex("dbo.TraningRoomByDevices", new[] { "TraningRoom_TraningRoomID" });
            DropColumn("dbo.TraningRoomByExercises", "TraningRoom_TraningRoomID");
            DropColumn("dbo.TraningRooms", "Character_CharacterID");
            DropColumn("dbo.TraningRoomByDevices", "TraningRoom_TraningRoomID");
            DropTable("dbo.ExerciseByDevices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExerciseByDevices",
                c => new
                    {
                        ExerciseByDeviceId = c.Guid(nullable: false),
                        Device_DeviceID = c.Guid(),
                        ExerciseCategory_ExerciseCategoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.ExerciseByDeviceId);
            
            AddColumn("dbo.TraningRoomByDevices", "TraningRoom_TraningRoomID", c => c.Guid());
            AddColumn("dbo.TraningRooms", "Character_CharacterID", c => c.Guid());
            AddColumn("dbo.TraningRoomByExercises", "TraningRoom_TraningRoomID", c => c.Guid());
            CreateIndex("dbo.TraningRoomByDevices", "TraningRoom_TraningRoomID");
            CreateIndex("dbo.TraningRooms", "Character_CharacterID");
            CreateIndex("dbo.TraningRoomByExercises", "TraningRoom_TraningRoomID");
            CreateIndex("dbo.ExerciseByDevices", "ExerciseCategory_ExerciseCategoryId");
            CreateIndex("dbo.ExerciseByDevices", "Device_DeviceID");
            AddForeignKey("dbo.ExerciseByDevices", "ExerciseCategory_ExerciseCategoryId", "dbo.ExerciseCategories", "ExerciseCategoryId");
            AddForeignKey("dbo.TraningRoomByExercises", "TraningRoom_TraningRoomID", "dbo.TraningRooms", "TraningRoomID");
            AddForeignKey("dbo.TraningRoomByDevices", "TraningRoom_TraningRoomID", "dbo.TraningRooms", "TraningRoomID");
            AddForeignKey("dbo.TraningRooms", "Character_CharacterID", "dbo.Characters", "CharacterID");
            AddForeignKey("dbo.ExerciseByDevices", "Device_DeviceID", "dbo.Devices", "DeviceID");
        }
    }
}
