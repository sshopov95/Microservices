using Microsoft.AspNetCore.Mvc;
using ReservationsApi.Interfaces;
using ReservationsApi.Models;

namespace ReservationsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IReservation _reservationService;
        public ReservationController(IReservation reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpGet]
        public async Task<IEnumerable<Reservation>> Get()
        {
            return await _reservationService.GetReservations();
        }

        [HttpPut("{id}")]
        public async Task Put(int id)
        {
            await _reservationService.UpdateMailStatus(id);
        }
    }
}
