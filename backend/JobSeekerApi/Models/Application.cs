namespace JobSeekerApi.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public int JobId { get; set; }
        public int UserId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; } = "Applied";

        public Job? Job { get; set; }
        public User? User { get; set; }
    }
}