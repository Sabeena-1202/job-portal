using System.ComponentModel.DataAnnotations;

namespace JobPortal.AdminService.DTOs
{
    public class JobCreateDto
    {
        [Required(ErrorMessage = "Job title is required")]
        public string JobTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company name is required")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Job description is required")]
        public string JobDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Salary range is required")]
        public string SalaryRange { get; set; } = string.Empty;
    }
}