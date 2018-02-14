namespace TechStore.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using TechStore.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TechStore.Models.EntityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TechStore.Models.EntityDbContext context)
        {
            //  This method will be called after migrating to the latest version.


            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            try
            {
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var RoleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

               // RoleManager.Create(new ApplicationRole() { Name = "HeadAdmin" });
               // RoleManager.Create(new ApplicationRole() { Name = "Admin" });
               // RoleManager.Create(new ApplicationRole() { Name = "User" });
               // UserManager.Create(new ApplicationUser() { EmailConfirmed = true, UserName = "HeadAdmin", Email = "HeadAdmin@live.com", PasswordHash = UserManager.PasswordHasher.HashPassword("password") });
                 context.Roles.AddOrUpdate(new ApplicationRole() { Name = "HeadAdmin" });
                 context.Roles.AddOrUpdate(new ApplicationRole() { Name = "Admin" });
                 context.Roles.AddOrUpdate(new ApplicationRole() { Name = "User" });
                 context.Users.AddOrUpdate(new ApplicationUser() { EmailConfirmed=true,UserName = "HeadAdmin", Email = "HeadAdmin@live.com", PasswordHash = UserManager.PasswordHasher.HashPassword("password") });
                 context.SaveChanges();
                var user = context.Users.SingleOrDefault(u => u.UserName == "HeadAdmin");
                var role = context.Roles.SingleOrDefault(r=>r.Name == "HeadAdmin");
                // var user = UserManager.FindByName("HeadAdmin");
                // var role = RoleManager.FindByName("HeadAdmin");
                if (!UserManager.IsInRole(user.Id, role.Id))
                {
                    UserManager.AddToRole(user.Id, role.Id);
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
