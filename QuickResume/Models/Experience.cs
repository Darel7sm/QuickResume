using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QuickResume.Models
{
    public class Experience
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required(ErrorMessage = "Job title is required")]
        [StringLength(100)]
        [DisplayName("Job Title")]
        public required string JobTitle { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        [StringLength(100)]
        [DisplayName("Company Name")]
        public required string CompanyName { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50)]
        public required string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(50)]
        public required string State { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }  

        [DisplayName("Currently Working Here")]
        public bool IsCurrentlyWorking { get; set; }

        [Required(ErrorMessage = "Job duty is required")]
        [StringLength(500)]
        [DisplayName("Job Duties")]
        public required string JobDuty { get; set; }
    }
}
