using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Data.Helpers;
using YamangTao.Model.RSP;

namespace YamangTao.Data.Repositories
{
    public class JobPositionRepository : IJobPositionRepository
    {
        private readonly DataContext _context;
        public JobPositionRepository(DataContext context)
        {
            _context = context;

        }
        public async Task AddAsync(JobPosition entity)
        {
           await _context.JobPositions.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<JobPosition> entities)
        {
            await _context.JobPositions.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<JobPosition>> GetAllJobPosition()
        {
            return await _context.JobPositions.ToListAsync();
        }

        public async Task<JobPosition> GetJobPosition(int id)
        {
            return await _context.JobPositions.FirstOrDefaultAsync(j => j.Id == id);
        }

        public void Remove(JobPosition entity)
        {
            _context.JobPositions.Remove(entity);
        }

        public void RemoveRange(IEnumerable<JobPosition> entities)
        {
            _context.JobPositions.RemoveRange(entities);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagedList<JobPosition>> SearchPositionsPaged(JobPositionParams positionParams)
        {
            var positions = _context.JobPositions.OrderBy(s => s.Name).AsQueryable();
            
            if (!string.IsNullOrEmpty(positionParams.Keyword))
            {
                positions = positions.Where(s => s.Name.ToUpper().Contains(positionParams.Keyword.ToUpper()));
            }

            if (!string.IsNullOrEmpty(positionParams.OrderBy))
            {
                switch (positionParams.OrderBy)
                {
                    case "name":
                    positions = positions.OrderBy(s => s.Name);
                    break;

                    case "salaryGrade":
                    positions = positions.OrderBy(s => s.SalaryGrade);
                    break;

                    default:
                    positions = positions.OrderBy(s => s.Name);
                    break;
                }
            }

            return await PagedList<JobPosition>.CreateAsync(positions, positionParams.PageNumber, positionParams.PageSize);
        }

        public async Task<bool> VerifyJobPosition(string positionName)
        {
            // return await _context.JobPositions.FirstOrDefaultAsync(j => j.Name.Equals(positionName)) != null;
            return await _context.JobPositions.AnyAsync(j => j.Name.Equals(positionName));
        }
        
    }
}