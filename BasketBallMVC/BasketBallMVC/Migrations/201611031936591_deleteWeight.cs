namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteWeight : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Characters", "Weight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Characters", "Weight", c => c.Int(nullable: false));
        }
    }
}
