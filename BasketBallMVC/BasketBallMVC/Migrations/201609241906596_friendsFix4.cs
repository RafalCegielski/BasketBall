namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friendsFix4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FriendInvitations", "InvitedUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendInvitations", "InvitingUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendLists", "InvitedUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendLists", "InvitingUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FriendInvitations", new[] { "InvitedUser_Id" });
            DropIndex("dbo.FriendInvitations", new[] { "InvitingUser_Id" });
            DropIndex("dbo.FriendLists", new[] { "InvitedUser_Id" });
            DropIndex("dbo.FriendLists", new[] { "InvitingUser_Id" });
            AddColumn("dbo.FriendInvitations", "InvitingUserEmail", c => c.String());
            AddColumn("dbo.FriendInvitations", "InvitedUserEmail", c => c.String());
            AddColumn("dbo.FriendLists", "InvitingUserEmail", c => c.String());
            AddColumn("dbo.FriendLists", "InvitedUserEmail", c => c.String());
            DropColumn("dbo.FriendInvitations", "InvitedUser_Id");
            DropColumn("dbo.FriendInvitations", "InvitingUser_Id");
            DropColumn("dbo.FriendLists", "InvitedUser_Id");
            DropColumn("dbo.FriendLists", "InvitingUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FriendLists", "InvitingUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.FriendLists", "InvitedUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.FriendInvitations", "InvitingUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.FriendInvitations", "InvitedUser_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.FriendLists", "InvitedUserEmail");
            DropColumn("dbo.FriendLists", "InvitingUserEmail");
            DropColumn("dbo.FriendInvitations", "InvitedUserEmail");
            DropColumn("dbo.FriendInvitations", "InvitingUserEmail");
            CreateIndex("dbo.FriendLists", "InvitingUser_Id");
            CreateIndex("dbo.FriendLists", "InvitedUser_Id");
            CreateIndex("dbo.FriendInvitations", "InvitingUser_Id");
            CreateIndex("dbo.FriendInvitations", "InvitedUser_Id");
            AddForeignKey("dbo.FriendLists", "InvitingUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FriendLists", "InvitedUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FriendInvitations", "InvitingUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FriendInvitations", "InvitedUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
