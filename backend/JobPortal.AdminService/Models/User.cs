namespace JobPortal.AdminService.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "JobSeeker";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}