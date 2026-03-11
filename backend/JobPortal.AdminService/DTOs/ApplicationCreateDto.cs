using System.ComponentModel.DataAnnotations;

namespace JobPortal.AdminService.DTOs
{
    public class ApplicationCreateDto
    {
        [Required(ErrorMessage = "JobId is required")]
        public int JobId { get; set; }
    }
}