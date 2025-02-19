using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QuickResume.Models
{
    public class Summary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required(ErrorMessage = "Position is required")]
        [DisplayName("Position Applying For")]
        [StringLength(50, ErrorMessage = "State cannot be longer than 50 characters")]
        public required string Position { get; set; }

        [Required(ErrorMessage = "Summary is required")]
        [StringLength(500, ErrorMessage = "Summary cannot exceed 500 characters")]
        [DisplayName("Professional Summary")]
        public required string Description { get; set; }
    }
}
