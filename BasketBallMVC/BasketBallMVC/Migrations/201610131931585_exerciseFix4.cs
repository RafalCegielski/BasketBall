namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exerciseFix4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TraningRoomByDevices", "TraningRoom_TraningRoomID", c => c.Guid());
            CreateIndex("dbo.TraningRoomByDevices", "TraningRoom_TraningRoomID");
            AddForeignKey("dbo.TraningRoomByDevices", "TraningRoom_TraningRoomID", "dbo.TraningRooms", "TraningRoomID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraningRoomByDevices", "TraningRoom_TraningRoomID", "dbo.TraningRooms");
            DropIndex("dbo.TraningRoomByDevices", new[] { "TraningRoom_TraningRoomID" });
            DropColumn("dbo.TraningRoomByDevices", "TraningRoom_TraningRoomID");
        }
    }
}
