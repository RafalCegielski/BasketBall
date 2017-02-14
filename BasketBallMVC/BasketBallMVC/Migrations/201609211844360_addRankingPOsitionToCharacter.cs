namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRankingPOsitionToCharacter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "RankingPosition", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "RankingPosition");
        }
    }
}
