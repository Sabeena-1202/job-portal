using JobPortal.AdminService.Models;

namespace JobPortal.AdminService.Repositories
{
    public interface IApplicationRepository
    {
        Task<List<Application>> GetAllApplicationsAsync();
        Task<Application?> GetByIdAsync(int applicationId);
        Task<bool> AlreadyAppliedAsync(int userId, int jobId);
        Task AddAsync(Application application);
        Task SaveChangesAsync();
    }
}