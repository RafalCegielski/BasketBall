namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCharacter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        CharacterID = c.Guid(nullable: false),
                        Weight = c.Int(nullable: false),
                        Strengh = c.Int(nullable: false),
                        Marksmanship = c.Int(nullable: false),
                        Defence = c.Int(nullable: false),
                        Experience = c.Int(nullable: false),
                        WinGames = c.Int(nullable: false),
                        LoseGames = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.CharacterID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Characters");
        }
    }
}
