using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Data.Helpers;
using YamangTao.Model.PM;
using System.Collections.Generic;

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

        public void DeleteRange<T>(IEnumerable<T> entities) where T : class
        {
            _context.RemoveRange(entities);
        }

        public async Task<Ipcr> GetIpcrByID(int id)
        {
            return await _context.Ipcrs
                            .Include(i => i.KPIs)
                            .FirstOrDefaultAsync(i => i.Id == id);
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
            // Filter by ratee
            if (!string.IsNullOrEmpty(ipcrParams.RateeId))
            {
                ipcrs = ipcrs.Where(s => s.EmployeeId.ToUpper().Contains(ipcrParams.RateeId.ToUpper()));
            }
            // Filter by Job Position
            if (ipcrParams.JobPositionId > 0)
            {
                ipcrs = ipcrs.Where(s => s.JobPositionId == ipcrParams.JobPositionId);
            }
            // Filter by Org Unit
            if (ipcrParams.OrgUnitId > 0)
            {
                ipcrs = ipcrs.Where(s => s.OrgUnitId == ipcrParams.OrgUnitId);
            }


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

        public async Task<PagedList<Ipcr>> GetIpcrs(IpcrParams ipcrParams, string empId)
        {
            // include all objects
            var ipcrs = _context.Ipcrs.Include(i => i.Ratee)
                                        .Include(i => i.ReviewedBy)
                                        .Include(i => i.CompiledBy)
                                        .Include(i => i.ApprovedBy)
                                        .Include(i => i.Position)
                                        .Include(i => i.Unit)
                                        .AsQueryable();
            
            ipcrs = ipcrs.Where(i => i.EmployeeId.Equals(empId));
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

        public async Task<Ipcr> GetIpcrWithChildrenById(int id)
        {
            return await _context.Ipcrs.Include(i => i.Ratee)
                                        .Include(i => i.ReviewedBy)
                                        .Include(i => i.CompiledBy)
                                        .Include(i => i.ApprovedBy)
                                        .Include(i => i.Position)
                                        .Include(i => i.Unit)
                                        .Include(i => i.KPIs)
                                        .ThenInclude(i => i.Kpis)
                                        .ThenInclude(i => i.Kpis)
                                        .ThenInclude(i => i.Kpis)
                                        .ThenInclude(i => i.RatingMatrices)
                                        .ThenInclude(i => i.Ratings)
                                        .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Kpi> GetKpiById(int id)
        {
            return await _context.KPIs
            .Include(k => k.Kpis)
                .FirstOrDefaultAsync( k => k.Id == id);
        }
        public async Task<Rating> GetRating(int rmId, sbyte rate)
        {
            return await _context.Ratings.FirstOrDefaultAsync(r => r.RatingMatrixId == rmId && r.Rate == rate);
        }

        public async Task<RatingMatrix> GetRatingMatrix(int id)
        {
            return await _context.RatingMatrix
                .Include(rm => rm.Ratings)
                .FirstOrDefaultAsync(rm => rm.Id == id);
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