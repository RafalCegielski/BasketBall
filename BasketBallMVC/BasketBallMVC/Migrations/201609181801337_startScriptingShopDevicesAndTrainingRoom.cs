namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class startScriptingShopDevicesAndTrainingRoom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraningRooms",
                c => new
                    {
                        TraningRoomID = c.Guid(nullable: false),
                        Character_CharacterID = c.Guid(),
                    })
                .PrimaryKey(t => t.TraningRoomID)
                .ForeignKey("dbo.Characters", t => t.Character_CharacterID)
                .Index(t => t.Character_CharacterID);
            
            CreateTable(
                "dbo.TraningRoomByDevices",
                c => new
                    {
                        TraningRoomByDeviceID = c.Guid(nullable: false),
                        Device_DeviceID = c.Guid(),
                        TraningRoom_TraningRoomID = c.Guid(),
                    })
                .PrimaryKey(t => t.TraningRoomByDeviceID)
                .ForeignKey("dbo.Devices", t => t.Device_DeviceID)
                .ForeignKey("dbo.TraningRooms", t => t.TraningRoom_TraningRoomID)
                .Index(t => t.Device_DeviceID)
                .Index(t => t.TraningRoom_TraningRoomID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraningRoomByDevices", "TraningRoom_TraningRoomID", "dbo.TraningRooms");
            DropForeignKey("dbo.TraningRoomByDevices", "Device_DeviceID", "dbo.Devices");
            DropForeignKey("dbo.TraningRooms", "Character_CharacterID", "dbo.Characters");
            DropIndex("dbo.TraningRoomByDevices", new[] { "TraningRoom_TraningRoomID" });
            DropIndex("dbo.TraningRoomByDevices", new[] { "Device_DeviceID" });
            DropIndex("dbo.TraningRooms", new[] { "Character_CharacterID" });
            DropTable("dbo.TraningRoomByDevices");
            DropTable("dbo.TraningRooms");
        }
    }
}
