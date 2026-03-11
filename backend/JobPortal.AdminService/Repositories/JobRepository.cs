using JobPortal.AdminService.Data;
using JobPortal.AdminService.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.AdminService.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly AppDbContext _context;

        public JobRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Job>> GetAllJobsAsync()
        {
            return await _context.Jobs
                .Include(j => j.Applications)
                .ToListAsync();
        }

        public async Task<Job?> GetByIdAsync(int jobId)
        {
            return await _context.Jobs
                .Include(j => j.Applications)
                .FirstOrDefaultAsync(j => j.JobId == jobId);
        }

        public async Task AddAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
        }

        public async Task<bool> JobExistsAsync(int jobId)
        {
            return await _context.Jobs
                .AnyAsync(j => j.JobId == jobId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}