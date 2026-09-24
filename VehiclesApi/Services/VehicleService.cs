using Microsoft.EntityFrameworkCore;
using VehiclesApi.Data;
using VehiclesApi.Interfaces;
using VehiclesApi.Models;

namespace VehiclesApi.Services
{
    public class VehicleService : IVehicle
    {
        private readonly ApiDbContext dbContext;

        public VehicleService()
        {
            dbContext = new ApiDbContext();
        }

        public async Task AddVehicle(Vehicle vehicle)
        {
            await dbContext.Vehicles.AddAsync(vehicle);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteVehicle(int id)
        {
            Vehicle? vehicle = await dbContext.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                dbContext.Vehicles.Remove(vehicle);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            List<Vehicle> vehicles = await dbContext.Vehicles.ToListAsync();
            return vehicles;
        }

        public async Task<Vehicle?> GetVehicleById(int id)
        {
            Vehicle? vehicle = await dbContext.Vehicles.FindAsync(id);
            return vehicle;
        }

        public async Task UpdateVehicle(int id, Vehicle vehicle)
        {
            Vehicle? vehicleObj = await dbContext.Vehicles.FindAsync(id);
            if (vehicleObj != null)
            {
                vehicleObj.Name = vehicle.Name;
                vehicleObj.ImageUrl = vehicle.ImageUrl;
                vehicleObj.Height = vehicle.Height;
                vehicleObj.Width = vehicle.Width;
                vehicleObj.MaxSpeed = vehicle.MaxSpeed;
                vehicleObj.Price = vehicle.Price;
                vehicleObj.Displacement = vehicle.Displacement;

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
