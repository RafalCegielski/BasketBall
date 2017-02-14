namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.Guid(nullable: false),
                        notificationType = c.Int(nullable: false),
                        notificationDetails = c.String(),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.NotificationId)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .Index(t => t.user_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "user_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Notifications", new[] { "user_Id" });
            DropTable("dbo.Notifications");
        }
    }
}
