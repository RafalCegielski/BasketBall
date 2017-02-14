namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeExercise2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Exercises", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exercises", "Name", c => c.String());
        }
    }
}
