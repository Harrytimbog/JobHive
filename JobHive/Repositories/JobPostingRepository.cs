using JobHive.Data;
using JobHive.Models;

namespace JobHive.Repositories
{
    public class JobPostingRepository : IRepository<JobPosting>
    {

        private readonly ApplicationDbContext _context;
        public JobPostingRepository(ApplicationDbContext context)
        {
            _context = context
        }
        public Task<JobPosting> AddAsync(JobPosting entity)
        {
            throw new NotImplementedException();
        }

        public Task<JobPosting> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<JobPosting>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<JobPosting> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<JobPosting> UpdateAsync(JobPosting entity)
        {
            throw new NotImplementedException();
        }
    }
}
