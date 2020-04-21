using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YamangTao.Core.Repository;
using YamangTao.Model;
using YamangTao.Model.RSP;
using YamangTao.Data.Core;

namespace YamangTao.Api.Controllers
{
    [Route("api/seeder")]
    [ApiController]
    public class SeederController : ControllerBase
    {
        private readonly IEmployeeRepository _repoEmp;
        private readonly IJobPositionRepository _jpRep;
        public SeederController(IEmployeeRepository empRep, IJobPositionRepository jpRep)
        {
            _jpRep = jpRep;
            _repoEmp = empRep;

        }

        [HttpPost("employees")]
        public async Task<IActionResult> SeedEmployees()
        {

            var employeeData = System.IO.File.ReadAllText("D:/Projects/dotnet3/YamangTao/Backend/YamangTao.Data/Seeders/json/usmemployees.json", Encoding.UTF8);
            var employees = JsonConvert.DeserializeObject<List<Employee>>(employeeData);

            foreach (var employee in employees)
            {
                _repoEmp.AddAsync(employee).Wait();
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
    }
}