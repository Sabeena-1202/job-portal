using JobSeekerApi.DTOs;

namespace JobSeekerApi.Services
{
    public interface ISeekerService
    {
        Task<List<JobDTO>> GetAllJobs();
        Task<JobDTO?> GetJobById(int jobId);
        Task<ApplicationResponseDTO?> ApplyForJob(int userId, ApplyJobDTO applyJobDTO);
        Task<List<ApplicationResponseDTO>> GetMyApplications(int userId);
    }
}