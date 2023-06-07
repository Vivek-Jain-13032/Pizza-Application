using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza.Models
{
    public class User
    {
        [Key] //Primary Key in database for user table
        [EmailAddress]
        [Required]
        public String Email { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String ContactNo { get; set; }

        [Required]
        public String Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }
    }
}
