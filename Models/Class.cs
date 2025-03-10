using System.ComponentModel.DataAnnotations;
namespace SchoolManagement.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int classNumber { get; set; }
        [Required]
        public string? ClassTeacherName { get; set; }
        public List<Students>? Students { get; set; } // Navigation property, means here students is a list of Students model. Each class can have multiple students. 
    }
}
