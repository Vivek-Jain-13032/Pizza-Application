using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Pizza.Models
{
    public class OrderPizza
    {
        [Required]
        public string Pizza_Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Size Size { get; set; }

        [Required]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Crust Crust { get; set; }

        [Required] 
        public List<string> Topping_Id { get; set; }

    }
}
