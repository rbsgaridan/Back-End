using System.Collections.Generic;
using System.Threading.Tasks;
using YamangTao.Core.HttpParams;
using YamangTao.Core.Repository;
using YamangTao.Data.Helpers;
using YamangTao.Model;
using YamangTao.Model.PM;

namespace YamangTao.Data.Core
{
    public interface IPmsRepository : IRepositoryFlex
    {
        Task<PagedList<Ipcr>> GetIpcrs(IpcrParams ipcrParams);
        Task<PagedList<Ipcr>> GetIpcrs(IpcrParams ipcrParams, string empId);
        Task<Ipcr> GetIpcrByID(int id);
        Task<Ipcr> GetIpcrWithChildrenById(int id);
        Task<Kpi> GetKpiById(int id);
        Task<RatingMatrix> GetRatingMatrix(int id);
        Task<Rating> GetRating(int rmId, sbyte rate);
    }
}