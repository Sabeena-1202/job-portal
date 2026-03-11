using System.ComponentModel.DataAnnotations;

namespace JobPortal.AdminService.DTOs
{
    public class ApplicationStatusUpdateDto
    {
        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("Applied|Under Review|Selected|Rejected",
            ErrorMessage = "Status must be: Applied, Under Review, Selected, or Rejected")]
        public string Status { get; set; } = string.Empty;
    }
}