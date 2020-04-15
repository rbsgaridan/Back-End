using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YamangTao.Core.HttpParams;
using YamangTao.Core.Repository;
using YamangTao.Data.Helpers;
using YamangTao.Model;

namespace YamangTao.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Employee entity)
        {
            await _context.Employees.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Employee> entities)
        {
            await _context.Employees.AddRangeAsync(entities);
        }

        public void Remove(Employee entity)
        {
            _context.Employees.Remove(entity);
        }
        public void RemoveRange(IEnumerable<Employee> entities)
        {
            _context.Employees.RemoveRange(entities);
        }

        public async Task<Employee> GetEmployeeByID(string id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<PagedList<Employee>> GetEmployees(EmployeeParams employeeParams)
        {
            var employees = _context.Employees.OrderBy(s => s.Lastname).AsQueryable();
            
            if (!string.IsNullOrEmpty(employeeParams.Keyword))
            {
                employees = employees.Where(s => s.Lastname.ToUpper().Contains(employeeParams.Keyword.ToUpper())
                                                || s.Firstname.ToUpper().Contains(employeeParams.Keyword.ToUpper())
                                                || s.MiddleName.ToUpper().Contains(employeeParams.Keyword.ToUpper())
                                                || s.MI.ToUpper().Contains(employeeParams.Keyword.ToUpper())
                                                );
            }

            if (!string.IsNullOrEmpty(employeeParams.OrderBy))
            {
                switch (employeeParams.OrderBy)
                {
                    case "lastname":
                    employees = employees.OrderBy(s => s.Lastname);
                    break;

                    case "firstname":
                    employees = employees.OrderBy(s => s.Firstname);
                    break;

                    case "middleName":
                    employees = employees.OrderBy(s => s.MiddleName);
                    break;

                    case "mI":
                    employees = employees.OrderBy(s => s.MI);
                    break;

                    default:
                    break;
                }
            }

            return await PagedList<Employee>.CreateAsync(employees, employeeParams.PageNumber, employeeParams.PageSize);
        }


        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
