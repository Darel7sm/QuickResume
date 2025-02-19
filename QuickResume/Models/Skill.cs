using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QuickResume.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required(ErrorMessage = "Skill name is required")]
        [StringLength(100)]
        [DisplayName("Skill Name")]
        public required string Name { get; set; }
    }
}
