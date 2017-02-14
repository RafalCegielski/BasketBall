namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLevelsAndMaxValues : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        LevelId = c.Guid(nullable: false),
                        Lvl = c.Int(nullable: false),
                        MaxHealth = c.Int(nullable: false),
                        MaxEnergy = c.Int(nullable: false),
                        MaxExperience = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LevelId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Levels");
        }
    }
}
