namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "isOpen", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "isOpen");
        }
    }
}
