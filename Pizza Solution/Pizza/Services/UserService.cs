using Microsoft.VisualBasic;
using Pizza.Exceptions;
using Pizza.Models;
using Pizza.Repository;
using Pizza.Utilities;
using System.Collections.Generic;

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
                throw new UserNotFoundException(Message.UserNotFound_ForLogin);
            }
            else if(email == customer.Email && password == customer.Password) {
                UserService.User_Id = customer.User_Id;
                token = jwtToken.CreateJwtToken(customer.Email, "user");
                return token;
            }
            else
            {
                throw new IncorrectEmailOrPasswordException(Message.Incorrect_EmailOrPassword);
            }
        }

        //Register User
        //OR throw appropriate exception if registration unsuccessfull
        public string Register(NewUser user)
        {
            var customer = _userDbContext.Customers.Any(u => u.Email == user.Email);
            int updateLines;
            if (customer)
            {
                throw new UserAlreadyExistsException(Message.User_Already_Exist);
            }

            var user1 = new NewUser()
            {
                User_Id = "u" + DateTime.Now.ToString("ss"),
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                ContactNo = user.ContactNo
            };

            //save user data in MS-SQL database
            _userDbContext.Customers.Add(user1);
            updateLines = _userDbContext.SaveChanges();

            //save user data in MongoDB database
            _mongoRepository.AddUser(new User()
            {
                User_Id = user1.User_Id,
                Email = user.Email,
                Orders = new List<OrderDetails>()
            });

            return user1.User_Id;
        }

        //Return user password if given user-id and email are correct.
        //OR throw appropriate exception if request unsuccessfull.
        public string ForgetPassword(string user_id, string email)
        {
            NewUser customer = _userDbContext.Customers.Find(user_id);
            if (customer == null)
            {
                throw new UserNotFoundException(Message.UserNotFound_ForForgetPassword);
            }
            else if(customer.User_Id == user_id && customer.Email == email)
            {
                return customer.Password;
            }
            else
            {
                throw new IncorrectEmailOrPasswordException(Message.Incorrect_EmailOrPassword);
            }
        }


        //Display Pizza Menu.
        public IEnumerable<Menu> ViewMenu()
        {
            return _mongoRepository.GetMenu();
        }

        //Create order and return amount.
        public string CreateOrder(List<OrderPizza> order)
        {
            User User = _mongoRepository.GetUserAndOrderDetails(User_Id);
            int amount = 0;

            //Check For Pizza Id and Topping Id
            foreach(OrderPizza orderItem in order)
            {
                var isPizzaAvailable = _mongoRepository.GetMenu()
                    .Any(menuItem => menuItem.Pizza
                    .Any(pizza => pizza.Pizza_Id == orderItem.Pizza_Id));


                var isToppingAvailable = _mongoRepository.GetMenu()
                    .Any(menuItem => menuItem.Toppings
                    .Any(topping => orderItem.Topping_Id.Contains(topping.Topping_Id)));

                if (!isPizzaAvailable)
                {
                    throw new PizzaNotFound(Message.PizzaNotFound);
                }
                else if (!isToppingAvailable)
                {
                    throw new ToppingNotFound(Message.ToppingNotFound);
                }
                else
                {
                    continue;
                }
            }

            //Calculate Amount.
            foreach (OrderPizza orderItem in order)
            {
                amount += (100 * orderItem.Quantity)
                    + (orderItem.Size == Size.Small ? 50 : (orderItem.Size == Size.Medium ? 100 : 150))
                    + (orderItem.Topping_Id.Count * 50);

            }

            var OrderDetails = new OrderDetails()
            {
                Order_Id = "o" + DateTime.Now.ToString("ss"),
                OrderPizzaDetails = order,
                OrderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
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
            return Message.Order_Placed+(amount+25);
        }
        
        //Track All Orders Which Are Not Delivered.
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
