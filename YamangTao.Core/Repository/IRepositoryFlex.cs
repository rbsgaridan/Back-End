using System.Collections.Generic;
using System.Threading.Tasks;

namespace YamangTao.Core.Repository
{
    public interface IRepositoryFlex
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         void DeleteRange <T>(IEnumerable<T> entities) where T: class;

         Task<bool> SaveAllAsync();
         bool SaveAll();
    }
}