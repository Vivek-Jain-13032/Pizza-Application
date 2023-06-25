using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Pizza.Models
{
    public class OrderDetails
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Order_Id { get; set; }
        public OrderPizza OrderPizzaDetails { get; set; }

        public string OrderDate { get; set; }

        public string OrderStatus { get; set; }

        public int OrderAmount { get; set; }

        public int OrderTax { get; set; }

        public int OrderSubTotal { get; set; }

        public string OrderDeliveryAgent { get; set; }

        public string OrderAgentID { get; set; }

        public string OrderAgentContact { get; set; }
    }
}
