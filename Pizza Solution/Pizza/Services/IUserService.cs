using Pizza.Models;

namespace Pizza.Services
{
    public interface IUserService
    {
        public string Register(NewUser user);
        public string Login(string email, string password);
        public string ForgetPassword(string user_id, string email);
        public IEnumerable<Menu> ViewMenu();
        public string CreateOrder(OrderPizza oreder);
        public List<OrderDetails> TrackOrder();
        public List<OrderDetails> OrderHistory();
    }
}
