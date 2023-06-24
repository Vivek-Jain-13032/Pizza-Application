using Pizza.Models;

namespace Pizza.Services
{
    public interface IManagerService
    {
        public string Login(string email, string password);
        //public void EditMenu(Menu menu);
        public List<OrderDetails> ViewAllOrders();
        public void ManageOrder(string order_id, string orderStatus);
        public OrderDetails OrderDetails(string order_id);

    }
}
