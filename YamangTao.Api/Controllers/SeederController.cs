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
using YamangTao.Model.OrgStructure;
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
        private readonly IOrgUnitRepository _ouRep;
        private readonly IBranchCampusRepository _bcRep;

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public SeederController(IEmployeeRepository empRep, 
                                IJobPositionRepository jpRep, 
                                UserManager<User> userManager, 
                                RoleManager<Role> roleManager,
                                IOrgUnitRepository ouRep,
                                IBranchCampusRepository bcRep)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _jpRep = jpRep;
            _repoEmp = empRep;
            _ouRep = ouRep;
            _bcRep = bcRep;

        }

        [HttpPost("employees")]
        public async Task<IActionResult> SeedEmployees()
        {

            var employeeData = System.IO.File.ReadAllText("D:/Projects/dotnet3/YamangTao/Backend/YamangTao.Data/Seeders/json/usmemployees.json", Encoding.UTF8);
            var employees = JsonConvert.DeserializeObject<List<Employee>>(employeeData);
            bool result = false;
            foreach (var employee in employees)
            {
                _repoEmp.AddAsync(employee).Wait();
                // result = await _repoEmp.SaveAll();    
            }

            result = await _repoEmp.SaveAll();
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

        [HttpPost("orgunits")]
        public async Task<IActionResult> SeedJobOrgUnits()
        {

            var orgUnitData = System.IO.File.ReadAllText("D:/Projects/dotnet3/YamangTao/Backend/YamangTao.Data/Seeders/json/OrgUnits.json", Encoding.UTF8);
            var orgUnit = JsonConvert.DeserializeObject<OrgUnit>(orgUnitData);

           
                _ouRep.AddAsync(orgUnit).Wait();
            

            var result = await _ouRep.SaveAll();
            if (result)
            {
                return Created("", null);
            }
            return BadRequest();
        }


        [HttpPost("branchcampus")]
        public async Task<IActionResult> Seedbranchcampus()
        {

            var branchcampusData = System.IO.File.ReadAllText("D:/Projects/dotnet3/YamangTao/Backend/YamangTao.Data/Seeders/json/branches.json", Encoding.UTF8);
            var branchcampuses = JsonConvert.DeserializeObject<List<BranchCampus>>(branchcampusData);

            foreach (var item in branchcampuses)
            {
                _bcRep.AddAsync(item).Wait();
            }
                
            var result = await _bcRep.SaveAll();
            if (result)
            {
                return Created("", null);
            }
            return BadRequest();
        }
    }
}