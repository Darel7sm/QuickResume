using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QuickResume.Models
{
    public class Social
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Url]
        [DisplayName("GitHub Link")]
        public string? GitHub { get; set; }

        [Url]
        [DisplayName("LinkedIn Link")]
        public string? LinkedIn { get; set; }

        [Url]
        [DisplayName("Personal Website")]
        public string? PersonalWebsite { get; set; }
    }
}
