using JobSeekerApi.Controllers;
using JobSeekerApi.Data;
using JobSeekerApi.DTOs;
using JobSeekerApi.Models;
using JobSeekerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobSeekerApi.Controllers
{
    [ApiController]
    [Route("api/seeker")]
    [Authorize(Roles = "JobSeeker")]
    public class SeekerController : ControllerBase
    {
        private readonly ISeekerService _seekerService;

        public SeekerController(ISeekerService seekerService)
        {
            _seekerService = seekerService;
        }

        // GET: api/seeker/jobs
        [HttpGet("jobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _seekerService.GetAllJobs();
            return Ok(jobs);
        }

        // GET: api/seeker/jobs/{id}
        [HttpGet("jobs/{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _seekerService.GetJobById(id);
            if (job == null)
                return NotFound(new { message = "Job not found!" });

            return Ok(job);
        }

        // POST: api/seeker/apply
        [HttpPost("apply")]
        public async Task<IActionResult> ApplyForJob([FromBody] ApplyJobDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _seekerService.ApplyForJob(userId, dto);

            if (result == null)
                return BadRequest(new { message = "Already applied or job not found!" });

            return Ok(result);
        }

        // GET: api/seeker/applications
        [HttpGet("applications")]
        public async Task<IActionResult> GetMyApplications()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var applications = await _seekerService.GetMyApplications(userId);
            return Ok(applications);
        }

        // GET: api/seeker/dashboard
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var applications = await _seekerService.GetMyApplications(userId);

            var dashboard = new
            {
                TotalApplications = applications.Count,
                Applied = applications.Count(a => a.Status == "Applied"),
                UnderReview = applications.Count(a => a.Status == "Under Review"),
                Selected = applications.Count(a => a.Status == "Selected"),
                Rejected = applications.Count(a => a.Status == "Rejected"),
                RecentApplications = applications.Take(5)
            };

            return Ok(dashboard);
        }
    }
}