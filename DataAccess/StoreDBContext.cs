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
        public StoreDBContext() : base("StoreDBContext") {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<StoreDBContext>());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
    
    }
   

}
