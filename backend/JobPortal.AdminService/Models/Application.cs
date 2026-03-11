namespace JobPortal.AdminService.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public int JobId { get; set; }
        public int UserId { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Applied";

        public Job Job { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}