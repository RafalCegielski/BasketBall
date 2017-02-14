namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exercise : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExerciseByDevices",
                c => new
                    {
                        ExerciseByDeviceId = c.Guid(nullable: false),
                        Device_DeviceID = c.Guid(),
                        ExerciseCategory_ExerciseCategoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.ExerciseByDeviceId)
                .ForeignKey("dbo.Devices", t => t.Device_DeviceID)
                .ForeignKey("dbo.ExerciseCategories", t => t.ExerciseCategory_ExerciseCategoryId)
                .Index(t => t.Device_DeviceID)
                .Index(t => t.ExerciseCategory_ExerciseCategoryId);
            
            CreateTable(
                "dbo.ExerciseCategories",
                c => new
                    {
                        ExerciseCategoryId = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ExerciseCategoryId);
            
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        ExerciseID = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Time = c.Int(nullable: false),
                        Experience = c.Int(nullable: false),
                        Result = c.Int(nullable: false),
                        ExerciseCategory_ExerciseCategoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.ExerciseID)
                .ForeignKey("dbo.ExerciseCategories", t => t.ExerciseCategory_ExerciseCategoryId)
                .Index(t => t.ExerciseCategory_ExerciseCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExerciseByDevices", "ExerciseCategory_ExerciseCategoryId", "dbo.ExerciseCategories");
            DropForeignKey("dbo.Exercises", "ExerciseCategory_ExerciseCategoryId", "dbo.ExerciseCategories");
            DropForeignKey("dbo.ExerciseByDevices", "Device_DeviceID", "dbo.Devices");
            DropIndex("dbo.Exercises", new[] { "ExerciseCategory_ExerciseCategoryId" });
            DropIndex("dbo.ExerciseByDevices", new[] { "ExerciseCategory_ExerciseCategoryId" });
            DropIndex("dbo.ExerciseByDevices", new[] { "Device_DeviceID" });
            DropTable("dbo.Exercises");
            DropTable("dbo.ExerciseCategories");
            DropTable("dbo.ExerciseByDevices");
        }
    }
}
