using System.ComponentModel.DataAnnotations;

namespace Pizza.Models
{
    public class Topping
    {
        [Key]
        [Required]
        public string Topping_Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Toppings { get; set; }
    }
}
