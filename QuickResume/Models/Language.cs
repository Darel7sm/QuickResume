using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QuickResume.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required(ErrorMessage = "Language name is required")]
        [StringLength(50)]
        [DisplayName("Language")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Proficiency level is required")]
        [StringLength(50)]
        [DisplayName("Proficiency Level")]
        public required string Proficiency { get; set; }  // e.g., Beginner, Intermediate, Fluent, Native
    }
}
