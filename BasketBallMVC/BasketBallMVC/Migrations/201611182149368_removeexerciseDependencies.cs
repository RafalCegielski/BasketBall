namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeexerciseDependencies : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExerciseDependencies", "ExerciseDependenciesId", "dbo.Exercises");
            DropIndex("dbo.ExerciseDependencies", new[] { "ExerciseDependenciesId" });
            DropTable("dbo.ExerciseDependencies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExerciseDependencies",
                c => new
                    {
                        ExerciseDependenciesId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ExerciseDependenciesId);
            
            CreateIndex("dbo.ExerciseDependencies", "ExerciseDependenciesId");
            AddForeignKey("dbo.ExerciseDependencies", "ExerciseDependenciesId", "dbo.Exercises", "ExerciseID");
        }
    }
}
