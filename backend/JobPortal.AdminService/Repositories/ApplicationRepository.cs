using JobPortal.AdminService.Data;
using JobPortal.AdminService.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.AdminService.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AppDbContext _context;

        public ApplicationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Application>> GetAllApplicationsAsync()
        {
            return await _context.Applications
                .Include(a => a.Job)
                .Include(a => a.User)
                .ToListAsync();
        }

        public async Task<Application?> GetByIdAsync(int applicationId)
        {
            return await _context.Applications
                .Include(a => a.Job)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);
        }

        public async Task<bool> AlreadyAppliedAsync(int userId, int jobId)
        {
            return await _context.Applications
                .AnyAsync(a => a.UserId == userId && a.JobId == jobId);
        }

        public async Task AddAsync(Application application)
        {
            await _context.Applications.AddAsync(application);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}