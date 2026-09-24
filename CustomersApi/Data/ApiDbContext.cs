using Microsoft.EntityFrameworkCore;
using CustomersApi.Models;

namespace CustomersApi.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CustomersApiDb");
        }
    }
}
