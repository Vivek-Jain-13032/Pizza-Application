using Pizza.Models;

namespace Pizza.Repository
{
    public interface IMongoRepository
    {
        IEnumerable<Menu> GetMenu();

        public void AddUser(User user);

        User GetUserAndOrderDetails(string User_Id);

        public void AddOrderDetailsToUser(User user);

        public void AddItemToMenu(Menu menu);

        public List<User> ViewAllOrders();

        public void SeedDataInMenu_mongodb();

    }
}