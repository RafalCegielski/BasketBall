namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exerciseFix3 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TraningRooms", "TraningRoomID");
            AddForeignKey("dbo.TraningRooms", "TraningRoomID", "dbo.Characters", "CharacterID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraningRooms", "TraningRoomID", "dbo.Characters");
            DropIndex("dbo.TraningRooms", new[] { "TraningRoomID" });
        }
    }
}
