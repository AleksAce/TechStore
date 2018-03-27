namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Manufacturer = c.String(),
                        DateAdded = c.DateTime(),
                        DateUpdated = c.DateTime(),
                        Description = c.String(),
                        PhotoURL = c.String(),
                        Price = c.Single(nullable: false),
                        IsInStock = c.Boolean(nullable: false),
                        LeftInStock = c.Int(nullable: false),
                        AvailableUntill = c.DateTime(),
                        AvailableInStockTime = c.Int(nullable: false),
                        Category_CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryID)
                .Index(t => t.Category_CategoryID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        customer_CustomerID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customers", t => t.customer_CustomerID)
                .Index(t => t.customer_CustomerID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        DateRegistered = c.DateTime(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        Order_OrderID = c.Int(nullable: false),
                        Product_ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_OrderID, t.Product_ProductID })
                .ForeignKey("dbo.Orders", t => t.Order_OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductID, cascadeDelete: true)
                .Index(t => t.Order_OrderID)
                .Index(t => t.Product_ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderProducts", "Product_ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderProducts", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "customer_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Products", "Category_CategoryID", "dbo.Categories");
            DropIndex("dbo.OrderProducts", new[] { "Product_ProductID" });
            DropIndex("dbo.OrderProducts", new[] { "Order_OrderID" });
            DropIndex("dbo.Orders", new[] { "customer_CustomerID" });
            DropIndex("dbo.Products", new[] { "Category_CategoryID" });
            DropTable("dbo.OrderProducts");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
