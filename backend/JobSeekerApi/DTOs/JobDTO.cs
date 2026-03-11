namespace JobSeekerApi.DTOs
{
    public class JobDTO
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string JobDescription { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string SalaryRange { get; set; } = string.Empty;
        public DateTime PostedDate { get; set; }
    }
}