namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeExercise3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TraningRoomByExercises", "ExerciseAtack_ExerciseID", c => c.Guid());
            AddColumn("dbo.TraningRoomByExercises", "ExerciseDefence_ExerciseID", c => c.Guid());
            CreateIndex("dbo.TraningRoomByExercises", "ExerciseAtack_ExerciseID");
            CreateIndex("dbo.TraningRoomByExercises", "ExerciseDefence_ExerciseID");
            AddForeignKey("dbo.TraningRoomByExercises", "ExerciseAtack_ExerciseID", "dbo.Exercises", "ExerciseID");
            AddForeignKey("dbo.TraningRoomByExercises", "ExerciseDefence_ExerciseID", "dbo.Exercises", "ExerciseID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraningRoomByExercises", "ExerciseDefence_ExerciseID", "dbo.Exercises");
            DropForeignKey("dbo.TraningRoomByExercises", "ExerciseAtack_ExerciseID", "dbo.Exercises");
            DropIndex("dbo.TraningRoomByExercises", new[] { "ExerciseDefence_ExerciseID" });
            DropIndex("dbo.TraningRoomByExercises", new[] { "ExerciseAtack_ExerciseID" });
            DropColumn("dbo.TraningRoomByExercises", "ExerciseDefence_ExerciseID");
            DropColumn("dbo.TraningRoomByExercises", "ExerciseAtack_ExerciseID");
        }
    }
}
