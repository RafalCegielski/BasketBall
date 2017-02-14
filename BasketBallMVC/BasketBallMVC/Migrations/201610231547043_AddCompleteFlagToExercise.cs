namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompleteFlagToExercise : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TraningRoomByExercises", "isComplete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TraningRoomByExercises", "isComplete");
        }
    }
}
