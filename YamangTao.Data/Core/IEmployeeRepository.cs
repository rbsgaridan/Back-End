using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Helpers;
using YamangTao.Model;

namespace YamangTao.Core.Repository
{
    public interface IEmployeeRepository : IRepositoryFlex
    {
        Task<PagedList<Employee>> GetEmployees(EmployeeParams employeeParams);
        Task<Employee> GetEmployeeByID(string id);
        Task<List<EmployeeName>> SearchEmployee(string keyword);
        Task<List<EmployeeName>> SearchEmployeeReturnProper(string keyword);

        Task<bool> VerifyEmployee(string lastname, string firstname, string id);
        Task<bool> VerifyEmployee(string lastname, string firstname);
        Task<bool> EmployeeExists(string lastname, string firstname, string middle);
        
        Task<bool> IdExists(string id);
        Task<List<string>> GetDistinctLastname();
        Task<List<string>> GetDistinctFirstname();
        Task<List<string>> GetDistinctMiddle();

    }
}
