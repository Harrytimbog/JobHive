using JobHive.Data;
using JobHive.Models;
using Microsoft.EntityFrameworkCore;

namespace JobHive.Repositories
{
    public class JobPostingRepository : IRepository<JobPosting>
    {

        private readonly ApplicationDbContext _context;
        public JobPostingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<JobPosting> AddAsync(JobPosting entity)
        {
            await _context.JobPostings.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<JobPosting> DeleteAsync(int id)
        {
            var jobPosting = await _context.JobPostings.FindAsync(id);
            if (jobPosting == null)
            {
                throw new KeyNotFoundException();
            }

            _context.JobPostings.Remove(jobPosting);
            await _context.SaveChangesAsync();
            return jobPosting;
        }

        public async Task<IEnumerable<JobPosting>> GetAllAsync()
        {
            return await  _context.JobPostings.ToListAsync();
        }

        public async Task<JobPosting> GetByIdAsync(int id)
        {
            var jobPosting = await _context.JobPostings.FindAsync(id);
            if (jobPosting == null)
            {
                throw new KeyNotFoundException();
            }

            return jobPosting;

        }

        public async Task<JobPosting> UpdateAsync(JobPosting entity)
        {
            _context.JobPostings.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
