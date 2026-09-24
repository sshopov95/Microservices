using Microsoft.AspNetCore.Mvc;
using VehiclesApi.Interfaces;
using VehiclesApi.Models;

namespace VehiclesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : Controller
    {
        private IVehicle _vehicleService;

        public VehiclesController(IVehicle vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IEnumerable<Vehicle>> Get()
        {
            return await _vehicleService.GetAllVehicles();
        }

        [HttpGet("{id}")]
        public async Task<Vehicle?> Get(int id)
        {
            return await _vehicleService.GetVehicleById(id);
        }

        [HttpPost]
        public async Task Post([FromBody] Vehicle vehicle)
        {
            await _vehicleService.AddVehicle(vehicle);
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Vehicle vehicle)
        {
            await _vehicleService.UpdateVehicle(id, vehicle);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _vehicleService.DeleteVehicle(id);
        }
    }
}
