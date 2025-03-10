using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Models
{
    public class Students
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? FatherName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Class must be between 1 and 12")]
        public int Standard { get; set; }

        public string? ProfileImage { get; set; } // Store the file path

        // Now we have to connect the Student model with the Class model
        [ForeignKey("Class")]
        public int ClassId { get; set; } // ClassId is same as the Id in the Class model. This is the foreign key.
        public Class? Class { get; set; } // Navigation property, means we can access the Class model from the Student model.
    }
}
