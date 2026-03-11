using JobPortal.AdminService.DTOs;

namespace JobPortal.AdminService.Services
{
    public interface IAdminService
    {
       
        Task<List<JobResponseDto>> GetAllJobsAsync();
        Task<JobResponseDto?> GetJobByIdAsync(int jobId);
        Task<string> CreateJobAsync(JobCreateDto jobCreateDto);
        Task<string> UpdateJobAsync(int jobId, JobCreateDto jobCreateDto);
        Task<string> DeleteJobAsync(int jobId);

        
        Task<List<UserResponseDto>> GetAllUsersAsync();

        
        Task<List<ApplicationResponseDto>> GetAllApplicationsAsync();
        Task<string> UpdateApplicationStatusAsync(int applicationId, ApplicationStatusUpdateDto statusDto);

        // Dashboard
        Task<DashboardStatsDto> GetDashboardStatsAsync();
    }
}