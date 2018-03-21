namespace DataAccess.Migrations
{
    using Models;
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

        protected override void Seed(DataAccess.StoreDBContext context)
        {
            Category category1 = new Category { Name = "Category1", Products = new List<Product>() };
            Category category2 = new Category { Name = "Category2", Products = new List<Product>() };

            Product product1;
            for (int i = 0; i < 100; i++)
            {
                product1 = new Product { Name = "Product" + i.ToString(), Category = new Category() };
                product1.Category = i % 2 == 0 ? category1 : category2;
                context.Products.AddOrUpdate(product1);
            }

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
