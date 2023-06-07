using Microsoft.EntityFrameworkCore;
using Pizza.Models;

namespace Pizza.Repository
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        //Create Tables in SQL for below Entites
        public DbSet<User> Customers { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Models.Pizza> Pizza { get; set; }
        public DbSet<Topping> Topping { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Pizza.Models.Pizza> pizzas = new List<Pizza.Models.Pizza>() {
                new Models.Pizza()
                {
                    Id = "1",

                }
            };
            base.OnModelCreating(modelBuilder);
            //Seed data for menu
            var menu = new List<Menu>()
            {
                new Menu()
                {
                   Tax = 10

                }
            };
        }*/

    }
}
