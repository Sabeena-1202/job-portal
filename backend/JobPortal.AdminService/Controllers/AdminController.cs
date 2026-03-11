using JobPortal.AdminService.DTOs;
using JobPortal.AdminService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.NetworkInformation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JobPortal.AdminService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var stats = await _adminService.GetDashboardStatsAsync();
            return Ok(stats);
        }

        
        [HttpGet("jobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _adminService.GetAllJobsAsync();
            return Ok(jobs);
        }

        
        [HttpGet("jobs/{jobId}")]
        public async Task<IActionResult> GetJobById(int jobId)
        {
            var job = await _adminService.GetJobByIdAsync(jobId);
            if (job == null)
                return NotFound(new { message = "Job not found" });

            return Ok(job);
        }

        
        [HttpPost("jobs")]
        public async Task<IActionResult> CreateJob([FromBody] JobCreateDto jobCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _adminService.CreateJobAsync(jobCreateDto);
            return Ok(new { message = result });
        }

    
        [HttpPut("jobs/{jobId}")]
        public async Task<IActionResult> UpdateJob(int jobId, [FromBody] JobCreateDto jobCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _adminService.UpdateJobAsync(jobId, jobCreateDto);

            if (result == "Job not found")
                return NotFound(new { message = result });

            return Ok(new { message = result });
        }

        
        [HttpDelete("jobs/{jobId}")]
        public async Task<IActionResult> DeleteJob(int jobId)
        {
            var result = await _adminService.DeleteJobAsync(jobId);

            if (result == "Job not found")
                return NotFound(new { message = result });

            return Ok(new { message = result });
        }

        
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsersAsync();
            return Ok(users);
        }

       
        [HttpGet("applications")]
        public async Task<IActionResult> GetAllApplications()
        {
            var applications = await _adminService.GetAllApplicationsAsync();
            return Ok(applications);
        }

        
        [HttpPut("applications/{applicationId}/status")]
        public async Task<IActionResult> UpdateApplicationStatus(
            int applicationId,
            [FromBody] ApplicationStatusUpdateDto statusDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _adminService
                .UpdateApplicationStatusAsync(applicationId, statusDto);

            if (result == "Application not found")
                return NotFound(new { message = result });

            return Ok(new { message = result });
        }
    }
}
