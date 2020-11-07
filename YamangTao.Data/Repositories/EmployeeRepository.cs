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
            return await _context.Employees
                .Include(e => e.CurrentCampus)
                .Include(e => e.CurrentUnit)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
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
                                                || s.Id.ToUpper().Contains(employeeParams.Keyword.ToUpper())
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

                    case "id":
                    employees = employees.OrderBy(s => s.MiddleName);
                    break;

                    default:
                    break;
                }
            }

            return await PagedList<Employee>.CreateAsync(employees, employeeParams.PageNumber, employeeParams.PageSize);
        }


        public async Task<bool> VerifyEmployee(string lastname, string firstname, string id)
        {   

            return await _context.Employees.AnyAsync(e => e.Lastname.ToUpper().Equals(lastname.ToUpper()) 
                                                    && e.Firstname.ToUpper().Equals(firstname.ToUpper())
                                                    && e.Id.Equals(id.ToUpper()) );
        }
        public async Task<bool> EmployeeExists(string lastname, string firstname, string middle)
        {   

            return await _context.Employees.AnyAsync(e => e.Lastname.ToUpper().Equals(lastname) 
                                                    && e.Firstname.ToUpper().Equals(firstname)
                                                    && e.MiddleName.ToUpper().Equals(middle) );
        }
        public async Task<bool> VerifyEmployee(string lastname, string firstname)
        {
            return await _context.Employees.AnyAsync(e => e.Lastname.ToUpper().Equals(lastname.ToUpper()) 
                                                    && e.Firstname.ToUpper().Equals(firstname.ToUpper()));
        }

        public async Task<bool> IdExists(string id)
        {
            return await _context.Employees.AnyAsync(e => e.Id.ToUpper().Equals(id.ToUpper()));
        }

        public async Task<List<EmployeeName>> SearchEmployee(string keyword)
        {
            if (keyword != null)
            {
                var employees = await _context.Employees.Where(e => e.Lastname.ToUpper().Contains(keyword.ToUpper())
                                                                    || e.Firstname.ToUpper().Contains(keyword.ToUpper())
                                                                    || e.MiddleName.ToUpper().Contains(keyword.ToUpper()))
                                        .Select(e => new EmployeeName {
                                            Id = e.Id,
                                            Name = String.Join(", ", e.Lastname, e.Firstname, e.MiddleName)})
                                        .Take(10)
                                        .ToListAsync();

                return employees;
            }
            return null;
        }

        public async Task<List<string>> GetDistinctLastname()
        {
            return await _context.Employees.Select(e => e.Lastname).Distinct().ToListAsync();
        }

        public async Task<List<string>> GetDistinctFirstname()
        {
            return await _context.Employees.Select(e => e.Firstname).Distinct().ToListAsync();
        }

        public async Task<List<string>> GetDistinctMiddle()
        {
            return await _context.Employees.Select(e => e.MiddleName).Distinct().ToListAsync();
        }

        public void Add<T>(T entity) where T : class
        {
           _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
             _context.Remove(entity);
        }

        public void DeleteRange<T>(IEnumerable<T> entities) where T : class
        {
            _context.RemoveRange(entities);
        }

        public async Task<T> GetById<T, K>(K id) where T : class
        {
            return await _context.FindAsync<T>(id);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<EmployeeName>> SearchEmployeeReturnProper(string keyword)
        {
            if (keyword != null)
            {
                var employees = await _context.Employees.Where(e => e.FullName.ToUpper().Contains(keyword.ToUpper()))
                                        .Select(e => new EmployeeName {
                                            Id = e.Id,
                                            Name = e.FullName})
                                        .Take(10)
                                        .ToListAsync();

                return employees;
            }
            return null;
        }

        public async Task<string> GenerateNewId(string status)
        {   
            string newid = "";
            int month = 0;
            int year = 0;
            string oldId = "";
            int last3digs = 0;
            const string cjc = "JC";
            // 1 - Determine status for the appropriate id format
            switch (status)
            {
                case "Contract-of-Service":
                case "Job Order":
                case "Contractual":
                    // Format [YY]-JC[MM][###]
                    // 2-1 Get the last record
                    var jc = await _context.Employees.Where(e => cjc.Contains(e.Id))
                                                       .OrderByDescending(e => e.Id)
                                                       .FirstOrDefaultAsync();
                    
                    if (jc == null)
                    {
                        newid = newid + DateTime.Now.ToString("yy");
                        newid = newid + "-JC" + DateTime.Now.Month.ToString("00");
                        newid = newid + String.Format("{0:000}", 1);
                        return newid;
                    }
                    // 2-2 Capture the ID number
                    oldId = jc.Id;
                    // 2-3 Extract month for the ID number
                    month = int.Parse(oldId.Substring(5,2));
                    year = int.Parse(oldId.Substring(0,2));
                    // 2-4 Extract the last 3 digits
                    last3digs = int.Parse(oldId.Substring(7,3));
                    // if current month is equal to the month from ID
                    if (DateTime.Now.Year != year)
                    {
                        newid = newid + DateTime.Now.ToString("yy");
                    }
                    else
                    {
                        newid = newid + year.ToString("00");
                    }

                    if (DateTime.Now.Month != month)
                    {
                        newid = newid + "-JC" + DateTime.Now.Month.ToString("00");
                        newid = newid + String.Format("{0:000}", 1);
                    }

                    else
                    {
                        newid = newid + "-JC" + month.ToString("00");
                        last3digs++;
                        newid = newid + last3digs.ToString("000");
                    }
                break;
                


                // Format [YY]-[MM][###]
                // 2-1 Get the last record
                default:
                
                    string temp1 = DateTime.Now.ToString("yy") + "-" + DateTime.Now.Month.ToString("00");
                     var perm = await _context.Employees.Where(e => e.Id.Contains(temp1))          
                                                        .OrderByDescending(e => e.Id)
                                                       .FirstOrDefaultAsync();

                     if (perm == null)
                     {
                        newid = newid + DateTime.Now.ToString("yy");
                        newid = newid + "-" + DateTime.Now.Month.ToString("00");
                        newid = newid + String.Format("{0:000}", 1);
                        return newid;
                     }

                    oldId = perm.Id;
                    // 2-3 Extract month for the ID number
                    month = int.Parse(oldId.Substring(3,2));
                    year = int.Parse(oldId.Substring(0,2));
                    // 2-4 Extract the last 3 digits
                    last3digs = int.Parse(oldId.Substring(5,3));
                    // if current month is equal to the month from ID
                    if (DateTime.Now.Year != year)
                    {
                        newid = newid + DateTime.Now.ToString("yy") + "-";
                    }
                    else
                    {
                        newid = newid + year.ToString("00") + "-";
                    }

                    if (DateTime.Now.Month != month)
                    {
                        newid = newid + DateTime.Now.Month.ToString("00");
                        newid = newid + String.Format("{0:000}", 1);
                    }

                    else
                    {
                        newid = newid + month.ToString("00");
                        last3digs++;
                        newid = newid + last3digs.ToString("000");
                    }

                break;
            }

            return newid;
        }
    }

    
}
