using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace QuickResume.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string UserId { get; set; }

        [DisplayName("Full Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Full Name is required")]
        public required string FullName { get; set; }

        [Required(ErrorMessage = "Please enter date of birth")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateofBirth { get; set; }


        [Required(ErrorMessage = "Please choose gender")]
        public required string Gender { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [StringLength(100)]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Your Email is not valid.")]
        [Remote("CheckUniqueEmailAddress", "RemoteValidation", AdditionalFields = nameof(Id))]
        [EmailAddress]
        public required string Email { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone number is required")]
        [DisplayName("Phone Number")]
        [Phone]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Street is required")]
        [StringLength(100, ErrorMessage = "Street cannot be longer than 100 characters")]
        public required string Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City cannot be longer than 50 characters")]
        public required string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(50, ErrorMessage = "State cannot be longer than 50 characters")]
        public required string Province { get; set; }

        [Required(ErrorMessage = "Zip Code is required")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Invalid Zip Code")]
        [DisplayName("Zip Code")]
        public required string ZipCode { get; set; }


        [Required(ErrorMessage = "Country is required")]
        [StringLength(50, ErrorMessage = "Country cannot be longer than 50 characters")]
        public required string Country { get; set; }

    }
}
