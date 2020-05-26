using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YamangTao.Core.Repository;
using YamangTao.Model;
using YamangTao.Model.RSP;
using YamangTao.Data.Core;
using YamangTao.Model.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace YamangTao.Api.Controllers
{
    [Route("api/seeder")]
    [ApiController]
    [Authorize(Policy="RequireAdminRole")]
    public class SeederController : ControllerBase
    {
        private readonly IEmployeeRepository _repoEmp;
        private readonly IJobPositionRepository _jpRep;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public SeederController(IEmployeeRepository empRep, IJobPositionRepository jpRep, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _jpRep = jpRep;
            _repoEmp = empRep;

        }

        [HttpPost("employees")]
        public async Task<IActionResult> SeedEmployees()
        {

            var employeeData = System.IO.File.ReadAllText("D:/Projects/dotnet3/YamangTao/Backend/YamangTao.Data/Seeders/json/usmemployees.json", Encoding.UTF8);
            var employees = JsonConvert.DeserializeObject<List<Employee>>(employeeData);
            // bool result = false;
            foreach (var employee in employees)
            {
                _repoEmp.AddAsync(employee).Wait();
                // result = await _repoEmp.SaveAll();    
            }

            var result = await _repoEmp.SaveAll();
            if (result)
            {
                return Created("", null);
            }
            return BadRequest();
        }

        [HttpPost("jobpositions")]
        public async Task<IActionResult> SeedJobPositions()
        {

            var positionData = System.IO.File.ReadAllText("D:/Projects/dotnet3/YamangTao/Backend/YamangTao.Data/Seeders/json/JobPositions.json", Encoding.UTF8);
            var positions = JsonConvert.DeserializeObject<List<JobPosition>>(positionData);

            foreach (var position in positions)
            {
                _jpRep.AddAsync(position).Wait();
            }

            var result = await _jpRep.SaveAll();
            if (result)
            {
                return Created("", null);
            }
            return BadRequest();
        }

        [HttpPost("seedall")]
        public async Task<IActionResult> SeedAdmin()
        {
            var roles = new List<Role>
                {
                   new Role{Id = "Employee", Name = "Employee"},
                    new Role{Id = "Department Head",Name = "Department Head"},
                    new Role{Id = "Unit Head",Name = "Unit Head"},
                    new Role{Id = "VP",Name = "VP"},
                    new Role{Id = "President",Name = "President"},
                    new Role{Id = "PMG",Name = "PMG"},
                    new Role{Id = "Planning",Name = "Planning"},
                    new Role{Id = "HR",Name = "HR"},
                    new Role{Id = "Admin",Name = "Admin"}

                };

            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }

            
            
            // Seed Admin
             var superUser = new User
                {
                    UserName = "admin@root",
                    Id = "admin@root"
                };
                
                IdentityResult result = _userManager.CreateAsync(superUser,"L!fe7352").Result;
                
                if (result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("admin@root").Result;
                    await _userManager.AddToRolesAsync(admin, new [] {"Admin", "Unit Head"});
                }

            return Ok("Seeded Roles and Admin");
        }
    }
}