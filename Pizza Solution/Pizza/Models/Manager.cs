using System.ComponentModel.DataAnnotations;

namespace Pizza.Models
{
    public class Manager
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
