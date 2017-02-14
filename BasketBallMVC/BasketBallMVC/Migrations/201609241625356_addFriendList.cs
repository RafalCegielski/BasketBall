namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFriendList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendInvitations",
                c => new
                    {
                        FriendInvitationId = c.Guid(nullable: false),
                        InvitingUserId = c.Guid(nullable: false),
                        InvitedUserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.FriendInvitationId);
            
            CreateTable(
                "dbo.FriendLists",
                c => new
                    {
                        FriendListID = c.Guid(nullable: false),
                        PlayerID = c.Guid(nullable: false),
                        User_UserID = c.Guid(),
                    })
                .PrimaryKey(t => t.FriendListID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .Index(t => t.User_UserID);
            
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
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Characters", t => t.Character_CharacterID)
                .Index(t => t.Character_CharacterID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendLists", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "Character_CharacterID", "dbo.Characters");
            DropIndex("dbo.Users", new[] { "Character_CharacterID" });
            DropIndex("dbo.FriendLists", new[] { "User_UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.FriendLists");
            DropTable("dbo.FriendInvitations");
        }
    }
}
