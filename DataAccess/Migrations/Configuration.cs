using Models;
namespace DataAccess.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.StoreDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "DataAccess.StoreDBContext";
        }

        protected override void Seed(StoreDBContext context)
        {
          //  context.Database.Delete();
           // context.Database.Create();
            base.Seed(context);
            context.Products.AddOrUpdate(new Product()
            {
                ProductID = 1,
                Name = "Product" + 1.ToString(),
                Categories = new List<Category>() {
                    new Category()
                    {
                    CategoryID = 1,
                    Name = "Category1",
                    },
                },
               
            });
            context.Orders.AddOrUpdate(new Order()
            {
                OrderDate = DateTime.Now,
                OrderID = 1,
                customer = new Customer()
                {
                    CustomerID = 1,
                    DateRegistered = DateTime.Now,
                    UserName = "Customer1"
                },
                ProductsOrderInfo = new List<ProductWithCompletedOrder>()
                {
                    new ProductWithCompletedOrder(){
                    productID = 1,
                    PricePayed = 20,
                    }
                }
            });



            context.SaveChanges();

        }
    }
    
}
