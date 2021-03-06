using System.Collections;
using System.Security.AccessControl;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Data.Helpers;
using YamangTao.Model.OrgStructure;
using YamangTao.Model;

namespace YamangTao.Data.Repositories
{
    public class OrgUnitRepository : IOrgUnitRepository
    {
        private readonly DataContext _context;
        public OrgUnitRepository(DataContext context)
        {
            _context = context;

        }
        public async Task AddAsync(OrgUnit entity)
        {
            await _context.OrgUnits.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<OrgUnit> entities)
        {
            await _context.OrgUnits.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<KeyValueItem<int,string>>> GetAllOrgUnit()
        {
            return await _context.OrgUnits
                        .Select(o => new KeyValueItem<int,string> { Key = o.Id, Value = o.UnitName })
                        .ToListAsync();
        }

        public async Task<IEnumerable<OrgUnit>> GetAllOrgUnitsWithChildren()
        {
            return await _context.OrgUnits.Include(o => o.OrgUnitChildren)
                                            .ThenInclude(c => c.OrgUnitChildren)
                                            .ThenInclude(o => o.OrgUnitChildren)
                                            .ThenInclude(o => o.OrgUnitChildren)
                                            .ThenInclude(o => o.OrgUnitChildren)
                                            
                                            .ToListAsync();
        }

        public async Task<OrgUnit> GetOrgUnit(int? id)
        {
            return await _context.OrgUnits.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<OrgUnit> GetOrgUnitWithChildren(int id)
        {
            return await _context.OrgUnits
                    .Include(o => o.ParentUnit)
                    .Include(o => o.OrgUnitChildren)
                    .ThenInclude(o => o.OrgUnitChildren)
                    .ThenInclude(o => o.OrgUnitChildren)
                    .ThenInclude(o => o.OrgUnitChildren)
                    .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<OrgUnit>> OrgUnitByUser(string employeeId)
        {
            return await _context.OrgUnits.Where(org => org.CurrentHeadId.Equals(employeeId)).ToListAsync();
        }

        public void Remove(OrgUnit entity)
        {
            _context.OrgUnits.Remove(entity);
        }

        public void RemoveRange(IEnumerable<OrgUnit> entities)
        {
            _context.OrgUnits.RemoveRange(entities);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagedList<OrgUnit>> SearchOrgUnitsPaged(OrgUnitParams orgUnitParams)
        {
            var orgUnits = _context.OrgUnits.OrderBy(s => s.UnitName).AsQueryable();
            
            if (!string.IsNullOrEmpty(orgUnitParams.Keyword))
            {
                orgUnits = orgUnits.Where(s => s.UnitName.ToUpper().Contains(orgUnitParams.Keyword.ToUpper())
                                                || s.Code.ToUpper().Contains(orgUnitParams.Keyword.ToUpper()));
            }

            if (!string.IsNullOrEmpty(orgUnitParams.OrderBy))
            {
                switch (orgUnitParams.OrderBy)
                {
                    case "code":
                    orgUnits = orgUnits.OrderBy(s => s.Code);
                    break;

                    case "unitName":
                    orgUnits = orgUnits.OrderBy(s => s.UnitName);
                    break;

                    default:
                    orgUnits = orgUnits.OrderBy(s => s.UnitName);
                    break;
                }
            }

            return await PagedList<OrgUnit>.CreateAsync(orgUnits, orgUnitParams.PageNumber, orgUnitParams.PageSize);
        }

        public async Task<bool> VerifyOrgUnit(string unitName)
        {
            return await _context.OrgUnits.AnyAsync(o => o.UnitName.ToUpper().Equals(unitName.ToUpper()));
        }

      
    }
}