using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReservationsApi.Data;
using ReservationsApi.Interfaces;
using ReservationsApi.Models;
using System.Net;
using System.Net.Mail;

namespace ReservationsApi.Services
{
    public class ReservationService : IReservation
    {
        private readonly ApiDbContext dbContext;
        public ReservationService()
        {
            dbContext = new ApiDbContext();
        }
        public async Task<List<Reservation>?> GetReservations()
        {
            // Azure reciver code here
            //Add Code for Azure messaging bus
            string connectionString = "Endpoint= ... ";
            string queueName = "queue_name";

            //using IAsyncDisposable ServiceBusClient -> "await using"
            await using ServiceBusClient client = new(connectionString);
            
            ServiceBusReceiver receiver = client.CreateReceiver(queueName);
            IReadOnlyList<ServiceBusReceivedMessage> receivedMessages = await receiver.ReceiveMessagesAsync(10);
            if (receivedMessages  == null) 
            {
                return null;
            }

            foreach (ServiceBusReceivedMessage recievedMessage in receivedMessages)
            {
                string body = recievedMessage.Body.ToString();
                Reservation? messageCreated = JsonConvert.DeserializeObject<Reservation>(body);
                if (messageCreated != null)
                {
                    await dbContext.Reservations.AddAsync(messageCreated);
                    await dbContext.SaveChangesAsync();
                }
            }

            return await dbContext.Reservations.ToListAsync();
        }

        public async Task UpdateMailStatus(int id)
        {
            Reservation? reservationInDb = await dbContext.Reservations.FirstOrDefaultAsync(x=> x.Id == id);
            if (reservationInDb != null && reservationInDb.IsMailSent == false && reservationInDb.Email != null)
            {
                //SMTP client code goes here
                SmtpClient smtpClient = new("smtp.blq.bla.com") {
                    Port= 587,
                    Credentials = new NetworkCredential("user","pass"),
                    EnableSsl = true
                };
                smtpClient.Send("from", reservationInDb.Email, "Vehicle Test Drive", "Your car is reserved for testdrive");


                reservationInDb.IsMailSent = true;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
