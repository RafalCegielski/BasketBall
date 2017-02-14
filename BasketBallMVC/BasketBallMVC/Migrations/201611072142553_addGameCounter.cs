namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGameCounter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "GameCounter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "GameCounter");
        }
    }
}
