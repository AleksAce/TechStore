using DataAccess.Services;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess
{

    public class StoreDBContext : DbContext
    {

        public StoreDBContext() : base("StoreDBContext")
        {///
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new StoreDBInitializer());
           // Database.SetInitializer(new DropCreateDatabaseAlways<StoreDBContext>());
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }


    }


    public class StoreDBInitializer : DropCreateDatabaseAlways<StoreDBContext>
    {
        protected override void Seed(StoreDBContext context)
        {

            Category category1 = new Category { Name = "Category1", Products = new List<Product>() };
            Category category2 = new Category { Name = "Category2", Products = new List<Product>() };

            Product product1;
            for(int i = 0; i < 100; i++)
            {
                product1 = new Product { Name = "Product" + i.ToString(), Category = new Category() };
                product1.Category = i % 2==0? category1 : category2;
                context.Products.Add(product1);
            }
  
            context.SaveChanges();
            base.Seed(context);
        }

    }
}
