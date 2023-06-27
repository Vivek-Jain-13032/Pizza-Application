using Microsoft.VisualBasic;
using Pizza.Exceptions;
using Pizza.Models;
using Pizza.Repository;

namespace Pizza.Services
{
    public class UserService : IUserService
    {
        private static string User_Id;
        private readonly IJwtToken jwtToken;
        private readonly UserDbContext _userDbContext;
        private readonly IMongoRepository _mongoRepository;
        public UserService(IJwtToken jwtToken, UserDbContext _userDbContext, IMongoRepository mongoRepository)
        {
            this._userDbContext = _userDbContext;
            _mongoRepository = mongoRepository;
            this.jwtToken = jwtToken;
        }

        //Login user.
        //OR throw appropriate exception if login unsuccessfull.
        public string Login(string email, string password)
        {
            string token;
            var customer = _userDbContext.Customers.FirstOrDefault(u=>u.Email == email);
            if (customer == null)
            {
                throw new UserNotFoundException("User Not Found With Given Email: "+email);
            }
            else if(email == customer.Email && password == customer.Password) {
                UserService.User_Id = customer.User_Id;
                token = jwtToken.CreateJwtToken(customer.Email, "user");
                return token;
            }
            else
            {
                throw new IncorrectEmailOrPasswordException("Incorrect Email or Password");
            }
        }

        //Register User
        //OR throw appropriate exception if registration unsuccessfull
        public string Register(NewUser user)
        {
            var customer = _userDbContext.Customers.FirstOrDefault(u => u.Email == user.Email);
            int updateLines;
            if (customer != null)
            {
                throw new UserAlreadyExistsException("User Alredy Exist With Given Email: "+user.Email);
            }

            var user1 = new NewUser()
            {
                User_Id = "u" + DateTime.Now.ToString("ss"),
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                ContactNo = user.ContactNo
            };
            try
            {
                _userDbContext.Customers.Add(user1);
                updateLines = _userDbContext.SaveChanges();
                _mongoRepository.AddUser(new User()
                {
                    User_Id = user1.User_Id,
                    Email = user.Email,
                    Orders = new List<OrderDetails>()
                });
            }catch(Exception ex)
            {
                throw new Exception();
            }
            if(updateLines > 0)
            {
                return user1.User_Id;
            }
            else
            {
                return null;
            }
        }

        //Return user password if given user-id and email are correct.
        //OR throw appropriate exception if request unsuccessfull.
        public string ForgetPassword(string user_id, string email)
        {
            NewUser customer = _userDbContext.Customers.Find(user_id);
            if (customer == null)
            {
                throw new UserNotFoundException("User Not Registered With Given Email or Id");
            }
            else if(customer.User_Id == user_id && customer.Email == email)
            {
                return customer.Password;
            }
            else
            {
                throw new IncorrectEmailOrPasswordException("Incorrect User-Id or Email");
            }
        }


        //Display Pizza Menu.
        public IEnumerable<Menu> ViewMenu()
        {
            return _mongoRepository.GetMenu();
        }

        //Create order and return amount.
        public string CreateOrder(OrderPizza order)
        {
            User User = _mongoRepository.GetUserAndOrderDetails(User_Id);
            int amount = (100 * order.Quantity)
                    + (order.Size == Size.Small ? 50 : (order.Size == Size.Medium ? 100 : 150))
                    + (order.Topping_Id.Count * 50);

            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("yyyy-MM-dd HH:mm:ss");

            var OrderDetails = new OrderDetails()
            {
                Order_Id = "o" + DateTime.Now.ToString("ss"),
                OrderPizzaDetails = order,
                OrderDate = formattedDateTime,
                OrderStatus = "Accepted",
                OrderAmount = amount,
                OrderTax = 25,
                OrderSubTotal = amount+25,

                OrderDeliveryAgent = "Arjun",
                OrderAgentID = "Arjun_01",
                OrderAgentContact = "7418565495"
            };
            
            User.Orders.Add(OrderDetails);
            _mongoRepository.AddOrderDetailsToUser(User);
            return "Order placed Successfully, Amount to pay: "+(amount+25);
        }

        //Return list of login user's order details (all orders which are not Delivered).
        public List<OrderDetails> TrackOrder()
        {
            User user = _mongoRepository.GetUserAndOrderDetails(User_Id);
            var orderDetailsList = new List<OrderDetails>();
            foreach (var item in user.Orders)
            {
                if(item.OrderStatus.ToLower() != "delivered")
                {
                    orderDetailsList.Add(item);
                }
            }
            return orderDetailsList;
        }

        //Return list of login user's order details.
        public List<OrderDetails> OrderHistory()
        {
            User user = _mongoRepository.GetUserAndOrderDetails(User_Id);
            var orderDetailsList = new List<OrderDetails>();
            foreach (var item in user.Orders)
            {
                orderDetailsList.Add(item);
            }
            return orderDetailsList;
        }
    }
}
