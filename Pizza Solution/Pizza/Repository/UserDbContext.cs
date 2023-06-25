using Microsoft.EntityFrameworkCore;
using Pizza.Models;

namespace Pizza.Repository
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        //Create a table named as Customers.
        public DbSet<NewUser> Customers { get; set; }

        //Create a table named as Manger.
        public DbSet<Manager> Manager { get; set; }

        //Seeding data in Ms SQL Server for manager table.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var manager = new Manager()
            {
                Email = "manager@gmail.com",
                Id = "m001",
                Password = "pass"
            };

            //Seed manager data to the database.
            modelBuilder.Entity<Manager>().HasData(manager);
        }

    }
}
