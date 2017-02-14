namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDeviceMarker : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceCategories", "deviceMarker", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeviceCategories", "deviceMarker");
        }
    }
}
