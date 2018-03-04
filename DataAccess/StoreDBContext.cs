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
            //Database.SetInitializer(new DropCreateDatabaseAlways<StoreDBContext>());
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }


    }


    public class StoreDBInitializer : DropCreateDatabaseAlways<StoreDBContext>
    {
        protected override void Seed(StoreDBContext context)
        {
            Product product1 = new Product { Name = "Product1", Category = new Category() };
            Product product2 = new Product { Name = "Product2", Category = new Category() };
            Product product3 = new Product { Name = "Product3", Category = new Category() };
            Product product4 = new Product { Name = "Product4", Category = new Category() };
            Product product5 = new Product { Name = "Product5", Category = new Category() };
            Product product6 = new Product { Name = "Product6", Category = new Category() };

            Category category1 = new Category { Name = "Category1", Products = new List<Product>() };
            Category category2 = new Category { Name = "Category2", Products = new List<Product>() };

            product1.Category = category1;
            product2.Category = category1;
            product3.Category = category1;
                                     
            product4.Category = category2;
            product5.Category = category2;
            product6.Category = category2;  
            
            
            context.Products.Add(product1);
            context.Products.Add(product2);
            context.Products.Add(product3);
            context.Products.Add(product4);
            context.Products.Add(product5);
            context.Products.Add(product6);
           
           
           
            context.SaveChanges();
            base.Seed(context);
        }

    }
}
