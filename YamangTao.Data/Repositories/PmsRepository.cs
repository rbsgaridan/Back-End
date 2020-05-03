using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Data.Helpers;
using YamangTao.Model.PM;

namespace YamangTao.Data.Repositories
{
    public class PmsRepository : IPmsRepository
    {
        private readonly DataContext _context;
        public PmsRepository(DataContext context)
        {
            _context = context;

        }
        public async void Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Ipcr> GetIpcrByID(int id)
        {
            return await _context.Ipcrs.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<PagedList<Ipcr>> GetIpcrs(IpcrParams ipcrParams)
        {
            // include all objects
            var ipcrs = _context.Ipcrs.Include(i => i.Ratee)
                                        .Include(i => i.ReviewedBy)
                                        .Include(i => i.CompiledBy)
                                        .Include(i => i.ApprovedBy)
                                        .Include(i => i.Position)
                                        .Include(i => i.Unit)
                                        .Include(i => i.KPIs)
                                        .ThenInclude(i => i.KpiType)
                                        .AsQueryable();
            
            // if search keyword is not emplty
            if (!string.IsNullOrEmpty(ipcrParams.Keyword))
            {
                ipcrs = ipcrs.Where(s => s.Ratee.FullName.ToUpper().Contains(ipcrParams.Keyword.ToUpper()));
            }

            // Filter
            

            // Sort
            if (!string.IsNullOrEmpty(ipcrParams.OrderBy))
            {
                switch (ipcrParams.OrderBy)
                {
                    case "ratee":
                    ipcrs = ipcrs.OrderBy(s => s.Ratee.Lastname);
                    break;

                    case "jobPosition":
                    ipcrs = ipcrs.OrderBy(s => s.Position.Name);
                    break;

                    default:
                    ipcrs = ipcrs.OrderBy(s => s.Ratee.FullName);
                    break;
                }
            }

            return await PagedList<Ipcr>.CreateAsync(ipcrs, ipcrParams.PageNumber, ipcrParams.PageSize);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 1;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}