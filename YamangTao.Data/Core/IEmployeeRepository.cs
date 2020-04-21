using System;
using System.Threading.Tasks;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Helpers;
using YamangTao.Model;

namespace YamangTao.Core.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<PagedList<Employee>> GetEmployees(EmployeeParams employeeParams);
        Task<Employee> GetEmployeeByID(string id);

        Task<bool> VerifyEmployee(string lastname, string firstname, string middleName);
        Task<bool> IdExists(string id);

    }
}
