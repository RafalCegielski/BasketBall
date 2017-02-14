namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDataToTrain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TraningRoomByExercises", "AtackTrainingFinish", c => c.DateTime(nullable: false));
            AddColumn("dbo.TraningRoomByExercises", "DefenceTrainingFinish", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TraningRoomByExercises", "DefenceTrainingFinish");
            DropColumn("dbo.TraningRoomByExercises", "AtackTrainingFinish");
        }
    }
}
