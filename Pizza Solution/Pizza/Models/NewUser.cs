using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza.Models
{
    public class NewUser
    {
        [Key]
        public string User_Id { get; set; }

        [EmailAddress]
        [Required]
        public String Email { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        [MinLength(10, ErrorMessage ="Mobile Number Should Be Valid")]
        [MaxLength(10, ErrorMessage = "Mobile Number Should Be Valid")]
        public String ContactNo { get; set; }

        [Required]
        public String Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }
    }
}
