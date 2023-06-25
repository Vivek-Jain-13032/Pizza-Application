using Pizza.Exceptions;
using Pizza.Models;
using Pizza.Repository;

namespace Pizza.Services
{
    public class ManagerService : IManagerService
    {
        private static string Id;
        private readonly UserDbContext _userDbContext;
        private readonly IMongoRepository _mongoRepository;
        private readonly IJwtToken jwtToken;


        public ManagerService(UserDbContext _userDbContext, IMongoRepository mongoRepository, IJwtToken jwtToken)
        {
            this._userDbContext = _userDbContext;
            _mongoRepository = mongoRepository;
            this.jwtToken = jwtToken;
        }

        public string Login(string email, string password)
        {
            string token;
            var manager = _userDbContext.Manager.FirstOrDefault(m => m.Email == email);
            if (manager == null)
            {
                throw new UserNotFoundException("Account Not Found With Given Email: " + email);
            }
            else if (email == manager.Email && password == manager.Password)
            {
                ManagerService.Id = manager.Email;
                token = jwtToken.CreateJwtToken(manager.Email, "manager");
                return "JWT Token: "+token;
            }
            else
            {
                throw new IncorrectEmailOrPasswordException("Incorrect Email or Password");
            }
        }

        public List<OrderDetails> ViewAllOrders()
        {
            List<OrderDetails> orderDetails = new List<OrderDetails>();
            List<User> allUsers = _mongoRepository.ViewAllOrders();
            foreach (var item in allUsers)
            {
                foreach (var item1 in item.Orders)
                {
                    orderDetails.Add(item1);
                }
            }
            return orderDetails;

        }

        public void ManageOrder(string order_id, string orderStatus)
        {
            bool flag = false;
            List<User> allUsers = _mongoRepository.ViewAllOrders();
            foreach (var item in allUsers)
            {
                foreach (var item1 in item.Orders)
                {
                    if(item1.Order_Id == order_id)
                    {
                        item1.OrderStatus = orderStatus;
                        _mongoRepository.AddOrderDetailsToUser(item);
                        flag = true;
                        return;
                    }
                }
            }
            if (!flag)
            {
                throw new OrderNotFound("Order Not Found With Given Order_Id: "+ order_id);
            }
        }

        public OrderDetails OrderDetails(string order_id)
        {
            OrderDetails orderDetails = null;
            List<User> allUsers = _mongoRepository.ViewAllOrders();
            foreach (var item in allUsers)
            {
                foreach (var item1 in item.Orders)
                {
                    if (item1.Order_Id == order_id)
                    {
                        orderDetails = item1;
                        break;
                    }
                }
            }
            if (orderDetails != null)
            {
                return orderDetails;
            }
            else
            {
                throw new OrderNotFound("Order Not Found With Given Order_Id: " + order_id);
            }
        }
    }
}
