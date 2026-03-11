using Microsoft.EntityFrameworkCore;
using JobSeekerApi.Data;
using JobSeekerApi.DTOs;
using JobSeekerApi.Models;

namespace JobSeekerApi.Services
{
    public class SeekerService : ISeekerService
    {
        private readonly AppDbContext _context;

        public SeekerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<JobDTO>> GetAllJobs()
        {
            return await _context.Jobs
                .Select(j => new JobDTO
                {
                    JobId = j.JobId,
                    JobTitle = j.JobTitle,
                    CompanyName = j.CompanyName,
                    JobDescription = j.JobDescription,
                    Location = j.Location,
                    SalaryRange = j.SalaryRange,
                    PostedDate = j.PostedDate
                })
                .ToListAsync();
        }

        public async Task<JobDTO?> GetJobById(int jobId)
        {
            var job = await _context.Jobs.FindAsync(jobId);
            if (job == null) return null;

            return new JobDTO
            {
                JobId = job.JobId,
                JobTitle = job.JobTitle,
                CompanyName = job.CompanyName,
                JobDescription = job.JobDescription,
                Location = job.Location,
                SalaryRange = job.SalaryRange,
                PostedDate = job.PostedDate
            };
        }

        public async Task<ApplicationResponseDTO?> ApplyForJob(int userId, ApplyJobDTO dto)
        {
            // Check if job exists
            var job = await _context.Jobs.FindAsync(dto.JobId);
            if (job == null) return null;

            // Check if already applied
            var existing = await _context.Applications
                .FirstOrDefaultAsync(a => a.JobId == dto.JobId && a.UserId == userId);
            if (existing != null) return null;

            // Create application
            var application = new Application
            {
                JobId = dto.JobId,
                UserId = userId,
                ApplicationDate = DateTime.UtcNow,
                Status = "Applied"
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return new ApplicationResponseDTO
            {
                ApplicationId = application.ApplicationId,
                JobId = job.JobId,
                JobTitle = job.JobTitle,
                CompanyName = job.CompanyName,
                Location = job.Location,
                ApplicationDate = application.ApplicationDate,
                Status = application.Status
            };
        }

        public async Task<List<ApplicationResponseDTO>> GetMyApplications(int userId)
        {
            return await _context.Applications
                .Include(a => a.Job)
                .Where(a => a.UserId == userId)
                .Select(a => new ApplicationResponseDTO
                {
                    ApplicationId = a.ApplicationId,
                    JobId = a.JobId,
                    JobTitle = a.Job!.JobTitle,
                    CompanyName = a.Job.CompanyName,
                    Location = a.Job.Location,
                    ApplicationDate = a.ApplicationDate,
                    Status = a.Status
                })
                .ToListAsync();
        }
    }
}