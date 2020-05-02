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
        Task<PagedList<Ipcr>> GetIpcrs(IpcrParams employeeParams);
        Task<Ipcr> GetIpcrByID(int id);
        
        
        
    }
}