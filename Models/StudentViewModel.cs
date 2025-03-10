using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models
{
    public class StudentViewModel
    {
        public string? Name { get; set; }

        [Required]
        public string? FatherName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Class must be between 1 and 12")]
        public int Standard { get; set; }

        public IFormFile? ImageFile { get; set; }

    }
}
