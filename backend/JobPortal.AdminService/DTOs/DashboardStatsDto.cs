namespace JobPortal.AdminService.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalJobs { get; set; }
        public int TotalUsers { get; set; }
        public int TotalApplications { get; set; }
        public int ActiveJobs { get; set; }
    }
}