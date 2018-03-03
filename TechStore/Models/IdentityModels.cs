using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TechStore.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }
    }
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName)
        {

        }
    }
    
    public class EntityDbContext : IdentityDbContext<ApplicationUser>
    {
        public EntityDbContext()
            : base("EntityDB", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new EntityInitializer());
            this.Configuration.LazyLoadingEnabled = true;
            

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        
        }
        
        public static EntityDbContext Create()
        {
            return new EntityDbContext();
        }


    }
    public class EntityInitializer : DropCreateDatabaseAlways<EntityDbContext>
    {
        protected override void Seed(EntityDbContext context)
        {
            base.Seed(context);
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

             RoleManager.Create(new ApplicationRole() { Name = "HeadAdmin" });
             RoleManager.Create(new ApplicationRole() { Name = "Admin" });
             RoleManager.Create(new ApplicationRole() { Name = "User" });
            UserManager.Create(new ApplicationUser() { UserName = "HeadAdmin", Email = "HeadAdmin@live.com" }, "password");
             var user = UserManager.FindByName("HeadAdmin");
             var role = RoleManager.FindByName("HeadAdmin");
            if (!UserManager.IsInRole(user.Id, role.Name))
            {
                UserManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}