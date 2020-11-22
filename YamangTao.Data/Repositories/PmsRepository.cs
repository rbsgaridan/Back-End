using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Data.Helpers;
using YamangTao.Model.PM;
using System.Collections.Generic;
using YamangTao.Core.Common;

namespace YamangTao.Data.Repositories
{
    public class PmsRepository : IPmsRepository
    {
        private readonly DataContext _context;
        public PmsRepository(DataContext context)
        {
            _context = context;

        }

        public void Add<T>(T entity) where T : class
        {
           _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
             _context.Remove(entity);
        }

        public void DeleteRange<T>(IEnumerable<T> entities) where T : class
        {
            _context.RemoveRange(entities);
        }

        public async Task<T> GetById<T, K>(K id) where T : class
        {
            return await _context.FindAsync<T>(id);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagedList<T>> GetPaged<T, K>(PmsParams pmsParams) where T: class, IIdentifyableEntity<K>
        {
            var entities = _context.Set<T>().AsQueryable();
            
          
            //Keyword
            if (!string.IsNullOrEmpty(pmsParams.Keyword) && !string.IsNullOrEmpty(pmsParams.Filter))
            {
                if (!string.IsNullOrEmpty(pmsParams.Filter1))
                {
                    entities = entities.Where(a => EF.Property<string>(a, pmsParams.Filter).Contains(pmsParams.Keyword)
                                                    || EF.Property<string>(a, pmsParams.Filter1).Contains(pmsParams.Keyword));
                }
                else
                {
                    entities = entities.Where(a => EF.Property<string>(a, pmsParams.Filter).Contains(pmsParams.Keyword));
                }
                
            }
            
            if (!string.IsNullOrEmpty(pmsParams.FilterByKey))
            {
               if (pmsParams.KeyBool != null)
               {
                   entities = entities.Where(a => EF.Property<bool>(a, pmsParams.FilterByKey) == pmsParams.KeyBool);
               }
               if (pmsParams.KeyInt != null)
               {
                   entities = entities.Where(a => EF.Property<int>(a, pmsParams.FilterByKey) == pmsParams.KeyInt);
               }
               if (pmsParams.Keyword != null)
               {
                   entities = entities.Where(a => EF.Property<string>(a, pmsParams.FilterByKey) == pmsParams.Keyword);
               }
                    
            }
            // Sort
            if (!string.IsNullOrEmpty(pmsParams.OrderBy))
            {
                entities = entities.OrderBy(a => EF.Property<string>(a, pmsParams.OrderBy));
               
            }

            return await PagedList<T>.CreateAsync(entities, pmsParams.PageNumber, pmsParams.PageSize);
        }

        // public async Task<Ipcr> GetIpcrWithCompleteKpisById(int id)
        // {
        //     return await _context.Ipcrs.Include(i => i.Ratee)
        //                                 .Include(i => i.ReviewedBy)
        //                                 .Include(i => i.CompiledBy)
        //                                 .Include(i => i.ApprovedBy)
        //                                 .Include(i => i.Position)
        //                                 .Include(i => i.Unit)
        //                                 .Include(i => i.KPIs)
        //                                 .ThenInclude(i => i.Kpis)
        //                                 .ThenInclude(i => i.Kpis)
        //                                 .ThenInclude(i => i.Kpis)
        //                                 .ThenInclude(i => i.RatingMatrices)
        //                                 .ThenInclude(i => i.Ratings)
        //                                 .FirstOrDefaultAsync(i => i.Id == id);
        // }

        public async Task<List<T>> GetList<T, K>(PmsParams pmsParams) where T: class, IIdentifyableEntity<K>
        {
            var entities = _context.Set<T>().AsQueryable();
            
          
            //Keyword
            if (!string.IsNullOrEmpty(pmsParams.Keyword) && !string.IsNullOrEmpty(pmsParams.Filter))
            {
                if (!string.IsNullOrEmpty(pmsParams.Filter1))
                {
                    entities = entities.Where(a => EF.Property<string>(a, pmsParams.Filter).Contains(pmsParams.Keyword)
                                                    || EF.Property<string>(a, pmsParams.Filter1).Contains(pmsParams.Keyword));
                }
                else
                {
                    entities = entities.Where(a => EF.Property<string>(a, pmsParams.Filter).Contains(pmsParams.Keyword));
                }
                
            }
            
            if (!string.IsNullOrEmpty(pmsParams.FilterByKey))
            {
               if (pmsParams.KeyBool != null)
               {
                   entities = entities.Where(a => EF.Property<bool>(a, pmsParams.FilterByKey) == pmsParams.KeyBool);
               }
               if (pmsParams.KeyInt != null)
               {
                   entities = entities.Where(a => EF.Property<int>(a, pmsParams.FilterByKey) == pmsParams.KeyInt);
               }
               if (pmsParams.Keyword != null)
               {
                   entities = entities.Where(a => EF.Property<string>(a, pmsParams.FilterByKey) == pmsParams.Keyword);
               }
                    
            }
            // Sort
            if (!string.IsNullOrEmpty(pmsParams.OrderBy))
            {
                entities = entities.OrderBy(a => EF.Property<string>(a, pmsParams.OrderBy));
               
            }

            return await entities.ToListAsync();
        }

        // public async Task<Rating> GetRating(int rmId, sbyte rate)
        // {
        //     var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.RatingMatrixId == rmId && r.Rate == rate);
        //     return rating;
        // }
    }
}