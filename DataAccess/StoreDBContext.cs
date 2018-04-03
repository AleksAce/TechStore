

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
            //this.Configuration.LazyLoadingEnabled = false;
            //Database.SetInitializer(new StoreDBInitializer());
            //Database.SetInitializer(new DropCreateDatabaseAlways<StoreDBContext>());
            

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductWithCompletedOrder> ProductsOrdered { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasMany(c => c.OrdersIssued).WithOptional(c => c.customer);
            base.OnModelCreating(modelBuilder);
        }
    }


    public class StoreDBInitializer : DropCreateDatabaseAlways<StoreDBContext>
    {
        protected override void Seed(StoreDBContext context)
        {

            base.Seed(context);
            context.Products.Add(new Product()
            {
                ProductID = 1,
                Name = "Product" + 1.ToString(),
                isForMainPage = true,
                Categories = new List<Category>() {
                    new Category()
                    {
                    CategoryID = 1,
                    Name = "Category1",
                    },
                },

            });
            context.Orders.Add(new Order()
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


            context.Products.Add(new Product()
            {
                ProductID = 2,
                Name = "Product" + 2.ToString(),
                isForMainPage = true,
                Categories = new List<Category>() {
                    new Category()
                    {
                    CategoryID = 1,
                    Name = "Category1",
                    },
                },

            });
            context.Orders.Add(new Order()
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
                    productID = 2,
                    PricePayed = 22,
                    }
                }
            });

            context.Products.Add(new Product()
            {
                ProductID = 3,
                Name = "Product" + 3.ToString(),
                isForMainPage = false,
                Categories = new List<Category>() {
                    new Category()
                    {
                    CategoryID = 1,
                    Name = "Category1",
                    },
                },

            });
            context.Orders.Add(new Order()
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
                    productID = 3,
                    PricePayed = 23,
                    }
                }
            });


            context.SaveChanges();
        }

    }
}

/*
  protected override void Seed(StoreDBContext context)
        {

            context.Database.Delete();
            context.Database.Create();

            base.Seed(context);
            Category category1 = new Category { Name = "Category1", Products = new List<Product>() };
            Category category2 = new Category { Name = "Category2", Products = new List<Product>() };
            Category category3 = new Category { Name = "Category3", Products = new List<Product>() };
            Customer customer1 = new Customer()
            {
                CustomerID = 1,
                UserName = "TestCustomer1",
                DateRegistered = DateTime.Now,
            };
            Customer customer2 = new Customer()
            {
                CustomerID = 2,
                UserName = "TestCustomer2",
                DateRegistered = DateTime.Now,
            };
            Customer customer3 = new Customer()
            {
                CustomerID = 3,
                UserName = "TestCustomer3",
                DateRegistered = DateTime.Now,
            };
            Order order1 = new Order()
            {
                OrderID = 1,
                OrderDate = DateTime.Now,
                customer = customer1,
            };
            Order order2 = new Order()
            {
                OrderID = 2,
                OrderDate = DateTime.Now,
                customer = customer1,
            };
            Order order3 = new Order()
            {
                OrderID = 3,
                OrderDate = DateTime.Now,
                customer = customer1,
            };
          
          
          
            for (int i = 0; i < 2; i++)
            {
                context.Products.AddOrUpdate(new Product()
                {
                    ProductID = i,
                    Name = "Product" + i.ToString(),
                    Category = i % 2 == 0 ? category1 : category2,
                    Orders = new List<Order>()
                    {
                       order1,
                       order2
                    },
                });
          
            }
            context.Products.AddOrUpdate(new Product()
            {
                ProductID = 3,
                Name = "Product" + 3.ToString(),
                Category = category3,
                Orders = new List<Order>()
                {
                   order3
                },
          
            });
          
            context.SaveChanges();
            
        }
    
 */
