namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedProductsOrderedFromOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderProducts", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderProducts", "Product_ProductID", "dbo.Products");
            DropIndex("dbo.OrderProducts", new[] { "Order_OrderID" });
            DropIndex("dbo.OrderProducts", new[] { "Product_ProductID" });
            DropTable("dbo.OrderProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        Order_OrderID = c.Int(nullable: false),
                        Product_ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_OrderID, t.Product_ProductID });
            
            CreateIndex("dbo.OrderProducts", "Product_ProductID");
            CreateIndex("dbo.OrderProducts", "Order_OrderID");
            AddForeignKey("dbo.OrderProducts", "Product_ProductID", "dbo.Products", "ProductID", cascadeDelete: true);
            AddForeignKey("dbo.OrderProducts", "Order_OrderID", "dbo.Orders", "OrderID", cascadeDelete: true);
        }
    }
}
