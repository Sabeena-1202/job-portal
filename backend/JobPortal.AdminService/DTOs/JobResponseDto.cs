namespace JobPortal.AdminService.DTOs
{
    public class JobResponseDto
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string JobDescription { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string SalaryRange { get; set; } = string.Empty;
        public DateTime PostedDate { get; set; }
        public bool IsActive { get; set; }
        public int TotalApplications { get; set; }
    }
}