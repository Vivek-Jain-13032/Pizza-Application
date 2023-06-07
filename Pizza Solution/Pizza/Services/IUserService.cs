using Pizza.Models;

namespace Pizza.Services
{
    public interface IUserService
    {
        public bool register(User user);
        public string login(string email, string password);
        /*public void logout();*/
        public string ForgetPassword(string email);


        //---------------------------------------------------------

        //public List ViewMenu();

    }
}
