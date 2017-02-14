namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEnergyToExercises : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "Energy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "Energy");
        }
    }
}
