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

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        //        public static implicit operator List<object>(OrderDetails v)
        //        {
        //            throw new NotImplementedException();
        //        }

        //public override string ToString()
        //{
        //    return $"Order_Id: {Order_Id}, OrderPizzaDetails: {OrderPizzaDetails}, OrderDate: {OrderDate}, OrderStatus: {OrderStatus}, OrderAmount: {OrderAmount}, OrderTax: {OrderTax}, OrderSubTotal: {OrderSubTotal}, OrderDeliveryAgent: {OrderDeliveryAgent}, OrderAgentID: {OrderAgentID}, OrderAgentContact: {OrderAgentContact}";
        //}


    }
}
