using System.ComponentModel.DataAnnotations;

namespace Pizza.Models
{
    public class Menu
    {
        [Required]
        [Key]
        public string Menu_Id { get; set; }
        [Required]
        public int Pizza_Id { get; set; }
        [Required]
        public int Topping_Id { get; set; }
        [Required]
        public int Tax { get; set; }

        //Navigation Properties
        public Pizza Pizza { get; set; }
        public Topping Toppings { get; set; }

    }
}
