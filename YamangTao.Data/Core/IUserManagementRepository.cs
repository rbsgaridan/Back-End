using System.Threading.Tasks;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Helpers;
using YamangTao.Model.Auth;

namespace YamangTao.Data.Core
{
    public interface IUserManagementRepository
    {
        Task<PagedList<UserWithRolesForDisplay>> GetUsersWithRolesPaged(UserParams userParams);
        
    }
}