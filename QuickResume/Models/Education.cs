using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QuickResume.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required(ErrorMessage = "Institution Name is required")]
        [StringLength(100)]
        [DisplayName("Institution Name")]
        public required string Institution { get; set; }

        [Required(ErrorMessage = "Degree is required")]
        [StringLength(100)]
        public required string Degree { get; set; }

        [Required(ErrorMessage = "Field of Study is required")]
        [StringLength(100)]
        [DisplayName("Field of Study")]
        public required string FieldOfStudy { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("End Date (or Expected)")]
        public DateTime EndDate { get; set; }
    }
}
