namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friendsFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FriendInvitations", "InvitedUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.FriendInvitations", "InvitingUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.FriendLists", "InvitedUser_UserID", c => c.Guid());
            AddColumn("dbo.FriendLists", "InvitingUser_UserID", c => c.Guid());
            CreateIndex("dbo.FriendInvitations", "InvitedUser_Id");
            CreateIndex("dbo.FriendInvitations", "InvitingUser_Id");
            CreateIndex("dbo.FriendLists", "InvitedUser_UserID");
            CreateIndex("dbo.FriendLists", "InvitingUser_UserID");
            AddForeignKey("dbo.FriendInvitations", "InvitedUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FriendInvitations", "InvitingUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FriendLists", "InvitedUser_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.FriendLists", "InvitingUser_UserID", "dbo.Users", "UserID");
            DropColumn("dbo.FriendInvitations", "InvitingUserId");
            DropColumn("dbo.FriendInvitations", "InvitedUserId");
            DropColumn("dbo.FriendLists", "PlayerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FriendLists", "PlayerID", c => c.Guid(nullable: false));
            AddColumn("dbo.FriendInvitations", "InvitedUserId", c => c.Guid(nullable: false));
            AddColumn("dbo.FriendInvitations", "InvitingUserId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.FriendLists", "InvitingUser_UserID", "dbo.Users");
            DropForeignKey("dbo.FriendLists", "InvitedUser_UserID", "dbo.Users");
            DropForeignKey("dbo.FriendInvitations", "InvitingUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendInvitations", "InvitedUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FriendLists", new[] { "InvitingUser_UserID" });
            DropIndex("dbo.FriendLists", new[] { "InvitedUser_UserID" });
            DropIndex("dbo.FriendInvitations", new[] { "InvitingUser_Id" });
            DropIndex("dbo.FriendInvitations", new[] { "InvitedUser_Id" });
            DropColumn("dbo.FriendLists", "InvitingUser_UserID");
            DropColumn("dbo.FriendLists", "InvitedUser_UserID");
            DropColumn("dbo.FriendInvitations", "InvitingUser_Id");
            DropColumn("dbo.FriendInvitations", "InvitedUser_Id");
        }
    }
}
