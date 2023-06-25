using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Pizza.Models
{
    public class Topping
    {
        [BsonId] //Define as key in mongoDB.
        [Required]
        public string Topping_Id { get; set; }
        [Required]
        public string Topping_Name { get; set; }
    }
}
