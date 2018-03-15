using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechStore.Models;

namespace TechStore.Controllers
{
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            _roleManager = roleManager;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        // GET: User
        public async Task<ActionResult> Index()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            foreach(var user in UserManager.Users.ToList())
            {
                string role_name = (await RoleManager.FindByIdAsync(user.Roles.First().RoleId)).Name;
                RolesEnum RoleResult ;
                Enum.TryParse(role_name,out RoleResult);
                users.Add(new UserViewModel(user) { Role = RoleResult  });
  
            }
            return View(users);
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            UserViewModel UserVM =  new UserViewModel(user);
            string role_name = (await RoleManager.FindByIdAsync(user.Roles.First().RoleId)).Name;
            RolesEnum RoleResult;
            Enum.TryParse(role_name, out RoleResult);
            UserVM.Role = RoleResult;
            return View(UserVM);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            try
            {
               
                var NewUser = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
                if (await UserManager.FindByNameAsync(model.UserName) == null)
                {
                    await UserManager.CreateAsync(NewUser, model.Password);
                    var CreatedUser =await UserManager.FindByNameAsync(model.UserName);
                    IdentityResult result = await UserManager.AddToRoleAsync(CreatedUser.Id, model.Role.ToString());
                    if (!result.Succeeded)
                    {
                        foreach (var err in result.Errors)
                        {
                            Console.WriteLine(err);
                        }
                    }
                }
                else
                {
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: User/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            UserViewModel UserVM = new UserViewModel(user);
            string role_name = (await RoleManager.FindByIdAsync(user.Roles.First().RoleId)).Name;
            RolesEnum RoleResult;
            Enum.TryParse(role_name, out RoleResult);
            UserVM.Role = RoleResult;
            return View(UserVM);
            
        }

        // POST: User/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(UserViewModel user)
        {
            try
            {
                var user_to_edit = await UserManager.FindByIdAsync(user.ID);
                user_to_edit.UserName = user.UserName;
                user_to_edit.Email = user_to_edit.Email;
                user_to_edit.Roles.Clear();
               
                await UserManager.UpdateAsync(user_to_edit);
                IdentityResult result =  await UserManager.AddToRoleAsync(user_to_edit.Id, user.Role.ToString());
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        Console.WriteLine(err);
                    }
                }
                 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            UserViewModel UserVM = new UserViewModel(user);
            string role_name = (await RoleManager.FindByIdAsync(user.Roles.First().RoleId)).Name;
            RolesEnum RoleResult;
            Enum.TryParse(role_name, out RoleResult);
            UserVM.Role = RoleResult;
            return View(UserVM);
        }

        // POST: User/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(UserViewModel user)
        {
            try
            {
                var User = await UserManager.FindByIdAsync(user.ID);
                await UserManager.DeleteAsync(User);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
