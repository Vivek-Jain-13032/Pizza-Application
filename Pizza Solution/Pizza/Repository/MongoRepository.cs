using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Pizza.Models;

namespace Pizza.Repository
{
    public class MongoRepository : IMongoRepository
    {
        private readonly IMongoCollection<Menu> _menuCollection;
        private readonly IMongoCollection<User> _userCollection;
        private readonly IOptions<MongoDBSettings> _dbSettings;

        public MongoRepository(IOptions<MongoDBSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(_dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_dbSettings.Value.DatabaseName);
            _menuCollection = mongoDatabase.GetCollection<Menu>(_dbSettings.Value.MenuCollectionName);
            _userCollection = mongoDatabase.GetCollection<User>(_dbSettings.Value.CustomerCollectionName);
        }

        //For seeding data of menu in mongodb databse.
        public void SeedDataInMenu_mongodb()
        {
            // Clear existing data (optional)
            _menuCollection.DeleteMany(new BsonDocument());

            // Insert new data
            var menu = new Menu
            {
                Pizza = new List<Pizza.Models.Pizza>
            {
                new Pizza.Models.Pizza { Pizza_Id = "p101", Pizza_Name = "Margherita" },
                new Pizza.Models.Pizza { Pizza_Id = "p102", Pizza_Name = "Mushroom and Olive" },
                new Pizza.Models.Pizza { Pizza_Id = "p103", Pizza_Name = "Grilled Cheese" }
            },
                Toppings = new List<Topping>
            {
                new Topping { Topping_Id = "t201", Topping_Name = "Pepperoni" },
                new Topping { Topping_Id = "t202", Topping_Name = "Sausage" },
                new Topping { Topping_Id = "t203", Topping_Name = "Mushrooms" },
                new Topping { Topping_Id = "t204", Topping_Name = "Mozzarella cheese" }
            },
                Crusts = new List<Crust>
            {
                Crust.StuffedCrust,
                Crust.CrackerCrust,
                Crust.FlatBreadCrust,
                Crust.ThinCrust
            },
                Sizes = new List<Size>
            {
                Size.Small,
                Size.Medium,
                Size.Large
            },
                Tax = 25
            };
            _menuCollection.InsertOne(menu);
        }

        //Get complete data of menu collection.
        public IEnumerable<Menu> GetMenu() => _menuCollection.Find(_=>true).ToList();

        //Inserting user in user collection with user email and empty order list.
        public void AddUser(User user)
        {
            Console.WriteLine("Adding User: "+ user.ToString());
            _userCollection.InsertOne(user);
        }

        //Retrieving user by user-id.
        public User GetUserAndOrderDetails(string User_Id) {
            return _userCollection.Find(a => a.User_Id == User_Id).FirstOrDefault();
        }

        //Adding and updating order details to user's order list
        public void AddOrderDetailsToUser(User user)
        {
            _userCollection.FindOneAndReplace(a => a.User_Id == user.User_Id, user);
        }

        //To edit menu in menu collection.
        public void AddItemToMenu(Menu menu)
        {
            _menuCollection.InsertOne(menu);
        }

        //Retrive all users present in database from user collection.
        public List<User> ViewAllOrders()
        {
            return _userCollection.Find(new BsonDocument()).ToList();
        }

    }
}
