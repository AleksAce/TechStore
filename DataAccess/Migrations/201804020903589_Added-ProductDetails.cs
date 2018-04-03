namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProductDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductWithCompletedOrders",
                c => new
                    {
                        ProductWithCompletedOrderID = c.Int(nullable: false, identity: true),
                        productID = c.Int(nullable: false),
                        PricePayed = c.Single(nullable: false),
                        order_OrderID = c.Int(),
                    })
                .PrimaryKey(t => t.ProductWithCompletedOrderID)
                .ForeignKey("dbo.Orders", t => t.order_OrderID)
                .Index(t => t.order_OrderID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductWithCompletedOrders", "order_OrderID", "dbo.Orders");
            DropIndex("dbo.ProductWithCompletedOrders", new[] { "order_OrderID" });
            DropTable("dbo.ProductWithCompletedOrders");
        }
    }
}
