using Pizza.Exceptions;
using Pizza.Migrations;
using Pizza.Models;
using Pizza.Repository;

namespace Pizza.Services
{
    public class UserService : IUserService
    {
        private readonly UserDbContext _userDbContext;
        private readonly IJwtToken jwtToken;
        public UserService(UserDbContext _userDbContext, IJwtToken jwtToken)
        {
            this._userDbContext = _userDbContext;
            this.jwtToken = jwtToken;
        }


        public string login(string email, string password)
        {
            var customer = _userDbContext.Customers.Find(email);
            if (customer == null)
            {
                throw new UserNotFoundException("User Not Found With Given Email: "+email);
            }
            else if(email == customer.Email && password == customer.Password) {
                return jwtToken.CreateJwtToken(customer, "user");
            }
            else
            {
                throw new IncorrectEmailOrPasswordException("Incorrect Email or Password");
            }
        }

        public bool register(User user)
        {
            //Check User Alredy Register or not
            var costumer = _userDbContext.Customers.Find(user.Email);
            if(costumer != null)
            {
                //User Alredy Register
                throw new UserAlreadyExistsException("User Alredy Exist With Given Email: "+user.Email);
            }

            var user1 = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                ContactNo = user.ContactNo
            };
            _userDbContext.Customers.Add(user1);
            var updateLines = _userDbContext.SaveChanges();
            if(updateLines > 0)
            {
                //register Successfully
                return true;
            }
            else
            {
                //Server Side Error
                return false;
            }
        }

/*        public void logout()
        {
            throw new NotImplementedException();
        }*/


        public string ForgetPassword(string email)
        {
            //Need to create a mail and send Password to user Email.
            User customer = _userDbContext.Customers.Find(email);
            if(customer != null)
            {
                return customer.Password;
            }
            throw new UserNotFoundException("User Not Registered With Given Email: "+email);
        }
    }
}
