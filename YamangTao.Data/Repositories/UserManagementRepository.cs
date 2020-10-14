using System.Threading.Tasks;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Data.Helpers;
using YamangTao.Model.Auth;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YamangTao.Data.Repositories
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly DataContext _context;
        public UserManagementRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<PagedList<UserWithRolesForDisplay>> GetUsersWithRolesPaged(UserParams userParams)
        {
            var entities = (from user in _context.Users orderby user.KnownAs
                                    select new UserWithRolesForDisplay
                                    {
                                        Id = user.Id,
                                        UserName = user.UserName,
                                        KnownAs = user.KnownAs,
                                        Created = user.Created,
                                        LastActive = user.LastActive,
                                        Roles = (from userRole in user.UserRoles
                                                  join role in _context.Roles
                                                 on userRole.RoleId
                                                 equals role.Id
                                                 select role.Name).ToList()
                                    }).AsQueryable();

            
            //Keyword
            if (!string.IsNullOrEmpty(userParams.Keyword))
            {
                    entities = entities.Where(a => a.KnownAs.Contains(userParams.Keyword)
                                                    || a.Id.Contains(userParams.Keyword));
            }


            return await PagedList<UserWithRolesForDisplay>.CreateAsync(entities, userParams.PageNumber, userParams.PageSize);

        }
    }
}