using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YamangTao.Core.Repository;
using YamangTao.Model;


namespace YamangTao.Api.Controllers
{
    [Route("api/seeder")]
    [ApiController]
    public class SeederController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        public SeederController(IEmployeeRepository repo)
        {
            _repo = repo;

        }

        [HttpPost("employees")]
        public async Task<IActionResult> SeedEmployees()
        {
           
            var employeeData = System.IO.File.ReadAllText("D:/Projects/dotnet3/YamangTao/Backend/YamangTao.Data/Seeders/json/usmemployees.json", Encoding.UTF8);
            var employees = JsonConvert.DeserializeObject<List<Employee>>(employeeData);

            foreach (var employee in employees)
            {
                _repo.AddAsync(employee).Wait();
            }

            var result = await _repo.SaveAll();
            if (result)
            {
                return Created("", null);
            }
            return BadRequest();
        }
    }
}