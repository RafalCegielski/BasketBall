namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceCategories",
                c => new
                    {
                        DeviceCategoryId = c.Guid(nullable: false),
                        name = c.String(),
                        ShopCategory_ShopCategoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.DeviceCategoryId)
                .ForeignKey("dbo.ShopCategories", t => t.ShopCategory_ShopCategoryId)
                .Index(t => t.ShopCategory_ShopCategoryId);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        DeviceID = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        DeviceCategory_DeviceCategoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.DeviceID)
                .ForeignKey("dbo.DeviceCategories", t => t.DeviceCategory_DeviceCategoryId)
                .Index(t => t.DeviceCategory_DeviceCategoryId);
            
            CreateTable(
                "dbo.ShopCategories",
                c => new
                    {
                        ShopCategoryId = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ShopCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceCategories", "ShopCategory_ShopCategoryId", "dbo.ShopCategories");
            DropForeignKey("dbo.Devices", "DeviceCategory_DeviceCategoryId", "dbo.DeviceCategories");
            DropIndex("dbo.Devices", new[] { "DeviceCategory_DeviceCategoryId" });
            DropIndex("dbo.DeviceCategories", new[] { "ShopCategory_ShopCategoryId" });
            DropTable("dbo.ShopCategories");
            DropTable("dbo.Devices");
            DropTable("dbo.DeviceCategories");
        }
    }
}
