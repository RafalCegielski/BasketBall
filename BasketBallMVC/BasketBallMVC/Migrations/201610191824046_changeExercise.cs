namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeExercise : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "Value", c => c.Int(nullable: false));
            AddColumn("dbo.Exercises", "Level", c => c.Int(nullable: false));
            AddColumn("dbo.Exercises", "Cost", c => c.Int(nullable: false));
            DropColumn("dbo.Exercises", "Description");
            DropColumn("dbo.Exercises", "Result");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exercises", "Result", c => c.Int(nullable: false));
            AddColumn("dbo.Exercises", "Description", c => c.String());
            DropColumn("dbo.Exercises", "Cost");
            DropColumn("dbo.Exercises", "Level");
            DropColumn("dbo.Exercises", "Value");
        }
    }
}
