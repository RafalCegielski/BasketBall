namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addJobId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TraningRoomByExercises", "jobId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TraningRoomByExercises", "jobId");
        }
    }
}
