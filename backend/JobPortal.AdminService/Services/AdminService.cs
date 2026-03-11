using JobPortal.AdminService.DTOs;
using JobPortal.AdminService.Models;
using JobPortal.AdminService.Repositories;

namespace JobPortal.AdminService.Services
{
    public class AdminService : IAdminService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;
        private readonly IApplicationRepository _applicationRepository;

        public AdminService(
            IJobRepository jobRepository,
            IUserRepository userRepository,
            IApplicationRepository applicationRepository)
        {
            _jobRepository = jobRepository;
            _userRepository = userRepository;
            _applicationRepository = applicationRepository;
        }

        public async Task<List<JobResponseDto>> GetAllJobsAsync()
        {
            var jobs = await _jobRepository.GetAllJobsAsync();
            return jobs.Select(j => new JobResponseDto
            {
                JobId = j.JobId,
                JobTitle = j.JobTitle,
                CompanyName = j.CompanyName,
                JobDescription = j.JobDescription,
                Location = j.Location,
                SalaryRange = j.SalaryRange,
                PostedDate = j.PostedDate,
                IsActive = j.IsActive,
                TotalApplications = j.Applications.Count
            }).ToList();
        }

        public async Task<JobResponseDto?> GetJobByIdAsync(int jobId)
        {
            var job = await _jobRepository.GetByIdAsync(jobId);
            if (job == null) return null;

            return new JobResponseDto
            {
                JobId = job.JobId,
                JobTitle = job.JobTitle,
                CompanyName = job.CompanyName,
                JobDescription = job.JobDescription,
                Location = job.Location,
                SalaryRange = job.SalaryRange,
                PostedDate = job.PostedDate,
                IsActive = job.IsActive,
                TotalApplications = job.Applications.Count
            };
        }

        public async Task<string> CreateJobAsync(JobCreateDto jobCreateDto)
        {
            var job = new Job
            {
                JobTitle = jobCreateDto.JobTitle,
                CompanyName = jobCreateDto.CompanyName,
                JobDescription = jobCreateDto.JobDescription,
                Location = jobCreateDto.Location,
                SalaryRange = jobCreateDto.SalaryRange,
                PostedDate = DateTime.UtcNow,
                IsActive = true
            };

            await _jobRepository.AddAsync(job);
            await _jobRepository.SaveChangesAsync();
            return "Job created successfully";
        }

        public async Task<string> UpdateJobAsync(int jobId, JobCreateDto jobCreateDto)
        {
            var job = await _jobRepository.GetByIdAsync(jobId);
            if (job == null) return "Job not found";

            job.JobTitle = jobCreateDto.JobTitle;
            job.CompanyName = jobCreateDto.CompanyName;
            job.JobDescription = jobCreateDto.JobDescription;
            job.Location = jobCreateDto.Location;
            job.SalaryRange = jobCreateDto.SalaryRange;

            await _jobRepository.SaveChangesAsync();
            return "Job updated successfully";
        }

        public async Task<string> DeleteJobAsync(int jobId)
        {
            var job = await _jobRepository.GetByIdAsync(jobId);
            if (job == null) return "Job not found";

            job.IsActive = false;
            await _jobRepository.SaveChangesAsync();
            return "Job deleted successfully";
        }

        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllJobSeekersAsync();
            return users.Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt,
                TotalApplications = u.Applications.Count
            }).ToList();
        }

        public async Task<List<ApplicationResponseDto>> GetAllApplicationsAsync()
        {
            var applications = await _applicationRepository.GetAllApplicationsAsync();
            return applications.Select(a => new ApplicationResponseDto
            {
                ApplicationId = a.ApplicationId,
                JobId = a.JobId,
                JobTitle = a.Job.JobTitle,
                CompanyName = a.Job.CompanyName,
                UserId = a.UserId,
                ApplicantName = a.User.Name,
                ApplicantEmail = a.User.Email,
                ApplicationDate = a.ApplicationDate,
                Status = a.Status
            }).ToList();
        }

        public async Task<string> UpdateApplicationStatusAsync(int applicationId, ApplicationStatusUpdateDto statusDto)
        {
            var application = await _applicationRepository.GetByIdAsync(applicationId);
            if (application == null) return "Application not found";

            application.Status = statusDto.Status;
            await _applicationRepository.SaveChangesAsync();
            return "Application status updated successfully";
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var jobs = await _jobRepository.GetAllJobsAsync();
            var users = await _userRepository.GetAllJobSeekersAsync();
            var applications = await _applicationRepository.GetAllApplicationsAsync();

            return new DashboardStatsDto
            {
                TotalJobs = jobs.Count,
                TotalUsers = users.Count,
                TotalApplications = applications.Count,
                ActiveJobs = jobs.Count(j => j.IsActive)
            };
        }
    }
}