namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exerciseFix5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TraningRoomByExercises", "TraningRoom_TraningRoomID", c => c.Guid());
            CreateIndex("dbo.TraningRoomByExercises", "TraningRoom_TraningRoomID");
            AddForeignKey("dbo.TraningRoomByExercises", "TraningRoom_TraningRoomID", "dbo.TraningRooms", "TraningRoomID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraningRoomByExercises", "TraningRoom_TraningRoomID", "dbo.TraningRooms");
            DropIndex("dbo.TraningRoomByExercises", new[] { "TraningRoom_TraningRoomID" });
            DropColumn("dbo.TraningRoomByExercises", "TraningRoom_TraningRoomID");
        }
    }
}
