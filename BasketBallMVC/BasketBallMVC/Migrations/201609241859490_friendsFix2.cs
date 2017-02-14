namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friendsFix2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Character_CharacterID", "dbo.Characters");
            DropForeignKey("dbo.FriendLists", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.FriendLists", "InvitedUser_UserID", "dbo.Users");
            DropForeignKey("dbo.FriendLists", "InvitingUser_UserID", "dbo.Users");
            DropIndex("dbo.FriendLists", new[] { "User_UserID" });
            DropIndex("dbo.FriendLists", new[] { "InvitedUser_UserID" });
            DropIndex("dbo.FriendLists", new[] { "InvitingUser_UserID" });
            DropIndex("dbo.Users", new[] { "Character_CharacterID" });
            DropColumn("dbo.FriendLists", "User_UserID");
            DropColumn("dbo.FriendLists", "InvitedUser_UserID");
            DropColumn("dbo.FriendLists", "InvitingUser_UserID");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Guid(nullable: false),
                        Nick = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Character_CharacterID = c.Guid(),
                    })
                .PrimaryKey(t => t.UserID);
            
            AddColumn("dbo.FriendLists", "InvitingUser_UserID", c => c.Guid());
            AddColumn("dbo.FriendLists", "InvitedUser_UserID", c => c.Guid());
            AddColumn("dbo.FriendLists", "User_UserID", c => c.Guid());
            CreateIndex("dbo.Users", "Character_CharacterID");
            CreateIndex("dbo.FriendLists", "InvitingUser_UserID");
            CreateIndex("dbo.FriendLists", "InvitedUser_UserID");
            CreateIndex("dbo.FriendLists", "User_UserID");
            AddForeignKey("dbo.FriendLists", "InvitingUser_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.FriendLists", "InvitedUser_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.FriendLists", "User_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Users", "Character_CharacterID", "dbo.Characters", "CharacterID");
        }
    }
}
