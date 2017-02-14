namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addChosenAttacks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChosenAttacks",
                c => new
                    {
                        ChosenAttacksId = c.Guid(nullable: false),
                        Exercise1 = c.Guid(nullable: false),
                        Exercise2 = c.Guid(nullable: false),
                        Exercise3 = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ChosenAttacksId)
                .ForeignKey("dbo.Characters", t => t.ChosenAttacksId)
                .Index(t => t.ChosenAttacksId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChosenAttacks", "ChosenAttacksId", "dbo.Characters");
            DropIndex("dbo.ChosenAttacks", new[] { "ChosenAttacksId" });
            DropTable("dbo.ChosenAttacks");
        }
    }
}
