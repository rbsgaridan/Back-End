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
    public class LndRepository : ILndRepository
    {
        private readonly DataContext _context;
        public LndRepository(DataContext context)
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

        public async Task<PagedList<T>> GetPaged<T, K>(LndParams lndParams) where T: class, IIdentifyableEntity<K>
        {
            var entities = _context.Set<T>().AsQueryable();
            
          
            //Keyword
            if (!string.IsNullOrEmpty(lndParams.Keyword) && !string.IsNullOrEmpty(lndParams.Filter))
            {
                if (!string.IsNullOrEmpty(lndParams.Filter1))
                {
                    entities = entities.Where(a => EF.Property<string>(a, lndParams.Filter).Contains(lndParams.Keyword)
                                                    || EF.Property<string>(a, lndParams.Filter1).Contains(lndParams.Keyword));
                }
                else
                {
                    entities = entities.Where(a => EF.Property<string>(a, lndParams.Filter).Contains(lndParams.Keyword));
                }
                
            }
            
            if (!string.IsNullOrEmpty(lndParams.FilterByKey))
            {
               if (lndParams.KeyBool != null)
               {
                   entities = entities.Where(a => EF.Property<bool>(a, lndParams.FilterByKey) == lndParams.KeyBool);
               }
               if (lndParams.KeyInt != null)
               {
                   entities = entities.Where(a => EF.Property<int>(a, lndParams.FilterByKey) == lndParams.KeyInt);
               }
               if (lndParams.Keyword != null)
               {
                   entities = entities.Where(a => EF.Property<string>(a, lndParams.FilterByKey) == lndParams.Keyword);
               }
                    
            }
            // Sort
            if (!string.IsNullOrEmpty(lndParams.OrderBy))
            {
                entities = entities.OrderBy(a => EF.Property<string>(a, lndParams.OrderBy));
               
            }

            return await PagedList<T>.CreateAsync(entities, lndParams.PageNumber, lndParams.PageSize);
        }

       

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


        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            var entities = await _context.Set<T>().ToListAsync();
            return entities;
        }
    }
}