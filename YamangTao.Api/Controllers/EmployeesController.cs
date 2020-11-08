using System.Collections;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Dto;
using YamangTao.Core.HttpParams;
using YamangTao.Core.Repository;
using System.Collections.Generic;
using YamangTao.Api.Helpers;
using YamangTao.Model;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace YamangTao.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        private bool HasValidRole(string employeeId)
        {
            var isCurrentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value.Equals(employeeId);
            if (!isCurrentUser)
            {
                if (!(User.IsInRole("Admin") || User.IsInRole("HR")))
                    {
                        return false;
                    }
            }
            return true;
        }


        [HttpGet("{id}", Name = "GetEmployee")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            if (!HasValidRole(id))
            {
                return Unauthorized("You do not have clearance to see what is not yours!");
            }
            //TODO: Implement Realistic Implementation
            var employee = await _repo.GetEmployeeByID(id);
            var employeeToReturn = _mapper.Map<EmployeeDto>(employee);
            return Ok(employeeToReturn);
        }


        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchEmployees([FromQuery] EmployeeParams employeeParams)
        {
            var employees = await _repo.SearchEmployee(employeeParams.Keyword);
            return Ok(employees);
        }

        [HttpGet("searchnames")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchEmployeNames([FromQuery] EmployeeParams employeeParams)
        {
            var employees = await _repo.SearchEmployeeReturnProper(employeeParams.Keyword);
            return Ok(employees);
        }

        [HttpGet("idexists/{id}")]
        public async Task<IActionResult> IDExists(string id)
        {
            return Ok(await _repo.IdExists(id));
        }



        [HttpGet]
        public async Task<IActionResult> GetEmployeesPaged([FromQuery] EmployeeParams employeeParams)
        {
            var employees = await _repo.GetEmployees(employeeParams);
            var employeesToReturn = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            Response.AddPagination(employees.CurrentPage, 
                                    employees.TotalCount, 
                                    employees.PageSize, 
                                    employees.TotalPages);
            return Ok(employeesToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, EmployeeDto employeeForUpdate)
        {
            var employeeFromRepo = await _repo.GetEmployeeByID(employeeForUpdate.Id);
            _mapper.Map(employeeForUpdate, employeeFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating Employee {employeeForUpdate.Id} failed on save.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeDto employeeForCreationDto)
        {
            // check if employee is already in the database
            if (await _repo.EmployeeExists(employeeForCreationDto.Lastname,
                                            employeeForCreationDto.Firstname,
                                            employeeForCreationDto.MiddleName))
            {
                throw new Exception($"{employeeForCreationDto.Lastname}, {employeeForCreationDto.Firstname} y {employeeForCreationDto.MiddleName} already exists");
            }

            var employee = _mapper.Map<Employee>(employeeForCreationDto);
            _repo.Add(employee);
            if (await _repo.SaveAllAsync())
            {
                var employeeToReturn = _mapper.Map<EmployeeDto>(employee);
                return CreatedAtRoute("GetEmployee", new { id = employee.Id }, employeeToReturn);
            }

            throw new Exception("Creating the employee failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteEmployee(string id)
        {
            if (!HasValidRole(id))
            {
                return Unauthorized("You do not have clearance to see what is not yours!");
            }

            var employeeFromRepo = await _repo.GetEmployeeByID(id);
            _repo.Delete(employeeFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the employee");
        }

        [HttpGet("names")]
        [Authorize(Policy="RequireHRrole")]
        public async Task<IActionResult> GetNames()
        {
            //TODO: Implement Realistic Implementation
          var newDto = new {
              Lastnames = await _repo.GetDistinctLastname(),
              Firstnames = await _repo.GetDistinctFirstname(),
              Middlenames = await _repo.GetDistinctMiddle()
          };
          return Ok(newDto);
        }

        [HttpGet("gennewid")]
        [Authorize(Policy="RequireHRrole")]
        public async Task<IActionResult> GenNewId([FromQuery] string status)
        {
           
           var newId = await _repo.GenerateNewId(status);
           return Ok(new { id = newId });
        }
    }
}
