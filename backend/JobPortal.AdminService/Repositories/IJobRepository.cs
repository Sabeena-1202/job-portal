using JobPortal.AdminService.Models;

namespace JobPortal.AdminService.Repositories
{
    public interface IJobRepository
    {
        Task<List<Job>> GetAllJobsAsync();
        Task<Job?> GetByIdAsync(int jobId);
        Task AddAsync(Job job);
        Task SaveChangesAsync();
        Task<bool> JobExistsAsync(int jobId);
    }
}