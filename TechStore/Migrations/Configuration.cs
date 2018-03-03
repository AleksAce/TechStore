namespace TechStore.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TechStore.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TechStore.Models.EntityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "TechStore.Models.EntityDbContext";
        }

        protected override void Seed(TechStore.Models.EntityDbContext context)
        {
            base.Seed(context);
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            if (!RoleManager.RoleExists("HeadAdmin"))
            {
                RoleManager.Create(new ApplicationRole() { Name = "HeadAdmin" });
                RoleManager.Create(new ApplicationRole() { Name = "Admin" });
                RoleManager.Create(new ApplicationRole() { Name = "User" });
            }
            if (UserManager.FindByName("HeadAdmin").Email != null)
            {
                UserManager.Create(new ApplicationUser() { UserName = "HeadAdmin", Email = "HeadAdmin@live.com" }, "password");
            }
            var user = UserManager.FindByName("HeadAdmin");
            var role = RoleManager.FindByName("HeadAdmin");
            if (!UserManager.IsInRole(user.Id, role.Name))
            {
                UserManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}

