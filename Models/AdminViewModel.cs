using System.ComponentModel.DataAnnotations;
namespace SchoolManagement.Models
{
    public class AdminViewModel
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
