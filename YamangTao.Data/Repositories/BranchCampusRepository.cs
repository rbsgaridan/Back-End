using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YamangTao.Data.Core;
using YamangTao.Model.OrgStructure;

namespace YamangTao.Data.Repositories
{
    public class BranchCampusRepository : IBranchCampusRepository
    {
        private readonly DataContext _context;
        public BranchCampusRepository(DataContext context)
        {
            _context = context;

        }
        public async Task AddAsync(BranchCampus entity)
        {
            await _context.BranchCampuses.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<BranchCampus> entities)
        {
             await _context.BranchCampuses.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<BranchCampus>> GetAllCampuses()
        {
            var campuses = await _context.BranchCampuses.ToListAsync();
            return campuses;
        }

        public async Task<BranchCampus> GetBranchCampus(int id)
        {
            var campus = await _context.BranchCampuses.FirstOrDefaultAsync(b => b.Id == id);
            return campus;
        }

        public Task<BranchCampus> GetBranchCampusWithEmployees(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(BranchCampus entity)
        {
            _context.BranchCampuses.Remove(entity);
        }
        

        public void RemoveRange(IEnumerable<BranchCampus> entities)
        {
            _context.BranchCampuses.RemoveRange(entities);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}