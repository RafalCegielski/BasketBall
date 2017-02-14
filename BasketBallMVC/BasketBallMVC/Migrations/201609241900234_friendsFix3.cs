namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friendsFix3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FriendLists", "InvitedUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.FriendLists", "InvitingUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.FriendLists", "InvitedUser_Id");
            CreateIndex("dbo.FriendLists", "InvitingUser_Id");
            AddForeignKey("dbo.FriendLists", "InvitedUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FriendLists", "InvitingUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendLists", "InvitingUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendLists", "InvitedUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FriendLists", new[] { "InvitingUser_Id" });
            DropIndex("dbo.FriendLists", new[] { "InvitedUser_Id" });
            DropColumn("dbo.FriendLists", "InvitingUser_Id");
            DropColumn("dbo.FriendLists", "InvitedUser_Id");
        }
    }
}
