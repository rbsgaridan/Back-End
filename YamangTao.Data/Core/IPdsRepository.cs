using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using YamangTao.Core.HttpParams;
using YamangTao.Core.Repository;
using YamangTao.Data.Helpers;
using YamangTao.Model.RSP.Pds;

namespace YamangTao.Data.Core
{
    public interface IPdsRepository : IRepositoryFlex
    {
        Task<PagedList<PersonalDataSheet>> GetPdsPaged(PdsParams pdsParams);

        Task<PersonalDataSheet> GetPdsByID(int id);
        Task<T> GetById<T>(int id) where T: class;
        Task<PersonalDataSheet> GetCompletePdsByID(int id);
        Task<IEnumerable<PersonalDataSheet>> GetAllPdsByEmployeeID(string employeeId);
        Task<PagedList<Address>> GetAddresses(PdsParams pdsParams);
        Task<PagedList<Identification>> GetIdCards(PdsParams pdsParams);
        Task<PagedList<Eligibility>> GetEligibilities(PdsParams pdsParams);
        
        // Calls for autocomplete
        // Address
        Task<IEnumerable<string>> SearchDistinctBlock(string keyword);
        Task<IEnumerable<string>> SearchDistinctStreet(string keyword);
        Task<IEnumerable<string>> SearchDistinctPurok(string keyword);
        Task<IEnumerable<string>> SearchDistinctBarangay(string keyword);
        Task<IEnumerable<string>> SearchDistinctMunicipality(string keyword);
        Task<IEnumerable<string>> SearchDistinctProvince(string keyword);
        
        // Eligibility
        Task<IEnumerable<string>> SearchDistinctEligibility(string keyword);
        Task<IEnumerable<string>> SearchDistinctExamPlace(string keyword);
        
        // ID Type
        Task<IEnumerable<string>> SearchDistinctIdTypes(string keyword);


        
    }
}