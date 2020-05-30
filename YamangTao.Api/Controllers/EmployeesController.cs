using System.Collections;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Api.Dtos;
using YamangTao.Core.HttpParams;
using YamangTao.Core.Repository;
using System.Collections.Generic;
using YamangTao.Api.Helpers;
using YamangTao.Model;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet("{id}", Name = "GetEmployee")]
        public async Task<IActionResult> GetEmployee(string id)
        {
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

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Updating Employee {employeeForUpdate.Id} failed on save.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeDto employeeForCreationDto)
        {
            // check if employee is already in the database
            if (await _repo.VerifyEmployee(employeeForCreationDto.Lastname,
                                            employeeForCreationDto.Firstname,
                                            employeeForCreationDto.MiddleName))
            {
                throw new Exception($"{employeeForCreationDto.Lastname}, {employeeForCreationDto.Firstname} y {employeeForCreationDto.MiddleName}");
            }

            var employee = _mapper.Map<Employee>(employeeForCreationDto);
            await _repo.AddAsync(employee);
            if (await _repo.SaveAll())
            {
                var employeeToReturn = _mapper.Map<EmployeeDto>(employee);
                return CreatedAtRoute("GetEmployee", new { id = employee.Id }, employeeToReturn);
            }

            throw new Exception("Creating the employee failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteEmployee(string id)
        {
            var employeeFromRepo = await _repo.GetEmployeeByID(id);
            _repo.Remove(employeeFromRepo);
            if (await _repo.SaveAll())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the employee");
        }
    }
}
