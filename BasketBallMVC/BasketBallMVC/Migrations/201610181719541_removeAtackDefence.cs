namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeAtackDefence : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ExerciseCategories", "isAtack");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExerciseCategories", "isAtack", c => c.Boolean(nullable: false));
        }
    }
}
