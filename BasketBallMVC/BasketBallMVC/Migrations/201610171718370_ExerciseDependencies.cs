namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExerciseDependencies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExerciseDependencies",
                c => new
                    {
                        ExerciseDependenciesId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ExerciseDependenciesId)
                .ForeignKey("dbo.Exercises", t => t.ExerciseDependenciesId)
                .Index(t => t.ExerciseDependenciesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExerciseDependencies", "ExerciseDependenciesId", "dbo.Exercises");
            DropIndex("dbo.ExerciseDependencies", new[] { "ExerciseDependenciesId" });
            DropTable("dbo.ExerciseDependencies");
        }
    }
}
