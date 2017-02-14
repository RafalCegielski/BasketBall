namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeCompleteFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TraningRoomByExercises", "IsCompleteAtack", c => c.Boolean(nullable: false));
            AddColumn("dbo.TraningRoomByExercises", "IsCompleteDefence", c => c.Boolean(nullable: false));
            DropColumn("dbo.TraningRoomByExercises", "IsComplete");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TraningRoomByExercises", "IsComplete", c => c.Boolean(nullable: false));
            DropColumn("dbo.TraningRoomByExercises", "IsCompleteDefence");
            DropColumn("dbo.TraningRoomByExercises", "IsCompleteAtack");
        }
    }
}
