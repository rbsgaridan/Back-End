using System.Collections.Generic;
using System.Threading.Tasks;
using YamangTao.Core.Common;
using YamangTao.Core.HttpParams;
using YamangTao.Core.Repository;
using YamangTao.Data.Helpers;
using YamangTao.Model;
using YamangTao.Model.PM;
using YamangTao.Model.PM.Template;

namespace YamangTao.Data.Core
{
    public interface IPmsRepository : IRepositoryFlex
    {
        Task<PagedList<T>> GetPaged<T,K>(PmsParams pmsParams) where T: class, IIdentifyableEntity<K>;
        Task<T> GetById<T,K>(K id) where T: class;
        Task<List<T>> GetList<T,K>(PmsParams pmsParams) where T: class, IIdentifyableEntity<K>;

        Task<KpiTemplate> GetKPITemplateFullById(int id);
        Task<List<KpiTemplate>> GetKPITemplateForIpcr(int id);

        // Task<Ipcr> GetIpcrWithCompleteKpisById(int id);

        // Task<Rating> GetRating(int rmId, sbyte rate);

    }
}