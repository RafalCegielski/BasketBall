namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exerciseFix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraningRoomByExercises",
                c => new
                    {
                        TraningRoomByExerciseID = c.Guid(nullable: false),
                        Exercise_ExerciseID = c.Guid(),
                        TraningRoom_TraningRoomID = c.Guid(),
                    })
                .PrimaryKey(t => t.TraningRoomByExerciseID)
                .ForeignKey("dbo.Exercises", t => t.Exercise_ExerciseID)
                .ForeignKey("dbo.TraningRooms", t => t.TraningRoom_TraningRoomID)
                .Index(t => t.Exercise_ExerciseID)
                .Index(t => t.TraningRoom_TraningRoomID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraningRoomByExercises", "TraningRoom_TraningRoomID", "dbo.TraningRooms");
            DropForeignKey("dbo.TraningRoomByExercises", "Exercise_ExerciseID", "dbo.Exercises");
            DropIndex("dbo.TraningRoomByExercises", new[] { "TraningRoom_TraningRoomID" });
            DropIndex("dbo.TraningRoomByExercises", new[] { "Exercise_ExerciseID" });
            DropTable("dbo.TraningRoomByExercises");
        }
    }
}
