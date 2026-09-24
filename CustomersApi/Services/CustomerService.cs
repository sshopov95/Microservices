using Azure.Messaging.ServiceBus;
using CustomersApi.Data;
using CustomersApi.Interfaces;
using CustomersApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

                //Add Code for Azure messaging bus
                string connectionString = "Endpoint= ... ";
                string queueName = "queue_name";

                //using IAsyncDisposable ServiceBusClient -> "await using"
                await using ServiceBusClient client = new ServiceBusClient(connectionString);

                //create sender
                ServiceBusSender sender = client.CreateSender(queueName);
                
                string json = JsonConvert.SerializeObject(customer);

                //create a a message. UTF-8 used
                ServiceBusMessage message = new ServiceBusMessage(json);

                //send message
                await sender.SendMessageAsync(message);
            }
        }
    }
}
