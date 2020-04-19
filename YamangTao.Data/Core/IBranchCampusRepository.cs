using System.Collections.Generic;
using System.Threading.Tasks;
using YamangTao.Core.Repository;
using YamangTao.Model.OrgStructure;

namespace YamangTao.Data.Core
{
    public interface IBranchCampusRepository : IRepository<BranchCampus>
    {
         Task<IEnumerable<BranchCampus>> GetAllCampuses();
         Task<BranchCampus> GetBranchCampus(int id);
         Task<BranchCampus> GetBranchCampusWithEmployees(int id);
    }
}