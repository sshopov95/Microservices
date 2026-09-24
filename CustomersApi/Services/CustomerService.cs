using CustomersApi.Data;
using CustomersApi.Interfaces;
using CustomersApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomersApi.Services
{
    public class CustomerService : ICustomer
    {
        private ApiDbContext dbContext;
        public CustomerService()
        {
            dbContext = new ApiDbContext();
        }
        public async Task AddCustomer(Customer customer)
        {
            if (customer.Vehicle != null)
            {
                Vehicle? vehicleInDb = await dbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == customer.VehicleId);
                if (vehicleInDb == null)
                {
                    await dbContext.Vehicles.AddAsync(customer.Vehicle);

                }
                //Already in DB
                customer.Vehicle = null;

                await dbContext.Customers.AddAsync(customer);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
