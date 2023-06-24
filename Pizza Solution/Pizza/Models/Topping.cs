using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Pizza.Models
{
    public class Topping
    {
        [Key]
        [BsonId]
        [Required]
        public string Topping_Id { get; set; }
        [Required]
        public string Topping_Name { get; set; }
        //[Required]
        //[BsonIgnore]
        //public int Toppings { get; set; }
    }
}
