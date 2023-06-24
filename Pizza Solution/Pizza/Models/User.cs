using MongoDB.Bson.Serialization.Attributes;

namespace Pizza.Models
{
    public class User
    {
        [BsonId]
        public string User_Id { get; set; }
        public String Email { get; set; }
        public List<OrderDetails> Orders { get; set; } = new List<OrderDetails>();

    }
}
