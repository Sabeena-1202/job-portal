namespace JobSeekerApi.DTOs
{
    public class ApplyJobDTO
    {
        public int JobId { get; set; }
    }

    public class ApplicationResponseDTO
    {
        public int ApplicationId { get; set; }
        public int JobId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}