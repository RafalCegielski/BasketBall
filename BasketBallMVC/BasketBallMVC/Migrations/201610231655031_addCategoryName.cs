namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCategoryName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TraningRoomByExercises", "ExerciseCategoryName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TraningRoomByExercises", "ExerciseCategoryName");
        }
    }
}
