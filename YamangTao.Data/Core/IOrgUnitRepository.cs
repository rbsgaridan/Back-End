using System.Collections.Generic;
using System.Threading.Tasks;
using YamangTao.Core.HttpParams;
using YamangTao.Core.Repository;
using YamangTao.Data.Helpers;
using YamangTao.Model.OrgStructure;

namespace YamangTao.Data.Core
{
    public interface IOrgUnitRepository : IRepository<OrgUnit>
    {
        Task<IEnumerable<OrgUnit>> GetAllOrgUnit();
        Task<IEnumerable<OrgUnit>> GetAllOrgUnitsWithChildren();
        Task<PagedList<OrgUnit>> SearchOrgUnitsPaged(OrgUnitParams positionParams);
        Task<OrgUnit> GetOrgUnit(int? id);
        Task<OrgUnit> GetOrgUnitWithChildren(int id);
        Task<bool> VerifyOrgUnit(string positionName);
    }
}