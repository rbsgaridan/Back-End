using System;

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using YamangTao.Model.Auth;

namespace YamangTao.Data.Seeders
{
    public class SeedUsers
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _context;

        public SeedUsers(UserManager<User> userManager, 
            RoleManager<Role> roleManager,
            DataContext context)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }

        public void SeedUsersAndRoles(string jsonPath)
        {
            if (!_userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText(jsonPath);
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                var roles = new List<Role>
                {
                    new Role{Name = "Employee"},
                    new Role{Name = "Department Head"},
                    new Role{Name = "Unit Head"},
                    new Role{Name = "VP"},
                    new Role{Name = "President"},
                    new Role{Name = "PMG"},
                    new Role{Name = "Planning"},
                    new Role{Name = "HR"},
                    new Role{Name = "Admin"}
                
                };

                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }

                foreach (var user in users)
                {
                    _userManager.CreateAsync(user, "password").Wait();
                    _userManager.AddToRoleAsync(user, "Member").Wait();
                }

                var superUser = new User
                {
                    UserName = "admin@root"
                };
                
                IdentityResult result = _userManager.CreateAsync(superUser,"password").Result;
                
                if (result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("admin@root").Result;
                    _userManager.AddToRolesAsync(admin, new [] {"Admin", "Director" }).Wait();;
                }


            }
        }
    }
}
