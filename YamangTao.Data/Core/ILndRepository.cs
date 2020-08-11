using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using YamangTao.Core.Common;
using YamangTao.Core.HttpParams;
using YamangTao.Core.Repository;
using YamangTao.Data.Helpers;

namespace YamangTao.Data.Core
{
    public interface ILndRepository : IRepositoryFlex
    {
        Task<PagedList<T>> GetPaged<T,K>(LndParams pmsParams) where T: class, IIdentifyableEntity<K>;
        Task<T> GetById<T,K>(K id) where T: class;
        Task<List<T>> GetList<T,K>(PmsParams pmsParams) where T: class, IIdentifyableEntity<K>;
        Task<IEnumerable<T>> GetAll<T>() where T: class;
        
    }
}