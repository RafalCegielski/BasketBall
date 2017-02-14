namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExerciseDependenciesChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExerciseCategories", "Distance", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExerciseCategories", "Distance");
        }
    }
}
