namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBuyDateToDevice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TraningRoomByDevices", "BuyDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TraningRoomByDevices", "BuyDate");
        }
    }
}
