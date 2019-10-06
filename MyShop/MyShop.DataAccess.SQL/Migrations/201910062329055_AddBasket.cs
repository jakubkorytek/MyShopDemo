namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBasket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        basketId = c.String(maxLength: 128),
                        productId = c.String(),
                        quantity = c.Int(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.basketId)
                .Index(t => t.basketId);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "basketId", "dbo.Baskets");
            DropIndex("dbo.BasketItems", new[] { "basketId" });
            DropTable("dbo.Baskets");
            DropTable("dbo.BasketItems");
        }
    }
}
