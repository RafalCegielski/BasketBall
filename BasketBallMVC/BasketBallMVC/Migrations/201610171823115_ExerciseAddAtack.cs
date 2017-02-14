namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExerciseAddAtack : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExerciseCategories", "isAtack", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExerciseCategories", "isAtack");
        }
    }
}
