using System.Collections.Generic;
using System.Threading.Tasks;
using YamangTao.Core.HttpParams;
using YamangTao.Core.Repository;
using YamangTao.Data.Helpers;
using YamangTao.Model.RSP;

namespace YamangTao.Data.Core
{
    public interface IJobPositionRepository : IRepository<JobPosition>
    {
        Task<IEnumerable<JobPosition>> GetAllJobPosition();
        Task<PagedList<JobPosition>> SearchPositionsPaged(JobPositionParams positionParams);
        Task<JobPosition> GetJobPosition(int id);
        Task<bool> VerifyJobPosition(string positionName);
    }
}