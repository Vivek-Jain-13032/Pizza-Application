using System.ComponentModel.DataAnnotations;

namespace Pizza.Models
{
    public class Pizza
    {
        [Key]
        [Required]
        public string Pizza_Id { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public string Crust { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
