using Pizza.Exceptions;
using Pizza.Models;
using Pizza.Repository;
using Pizza.Utilities;

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

        //Login Manager.
        //OR throw appropriate exception if login unsuccessfull.
        public string Login(string email, string password)
        {
            string token;
            var manager = _userDbContext.Manager.FirstOrDefault(m => m.Email == email);
            if (manager == null)
            {
                throw new UserNotFoundException(Message.UserNotFound_ForLogin);
            }
            else if (email == manager.Email && password == manager.Password)
            {
                ManagerService.Id = manager.Email;
                token = jwtToken.CreateJwtToken(manager.Email, "manager");
                return token;
            }
            else
            {
                throw new IncorrectEmailOrPasswordException(Message.Incorrect_EmailOrPassword);
            }
        }

        //Return list of all orders of all registered users.
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

        //Update order status by order-id.
        //OR throw exception if order not found.
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
                throw new OrderNotFound(Message.OrderNotFound + order_id);
            }
        }

        //Return complete order details based on order-id.
        //OR throw exception if order not found.
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
                throw new OrderNotFound(Message.OrderNotFound + order_id);
            }
        }
    }
}
