namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCharacter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "Gold", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "Energy", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "Level", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "Health", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "Health");
            DropColumn("dbo.Characters", "Level");
            DropColumn("dbo.Characters", "Energy");
            DropColumn("dbo.Characters", "Gold");
        }
    }
}
