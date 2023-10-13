using APIParqueadero.Models;
using APIParqueadero.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIParqueadero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpPost("register-entry")]
        public IActionResult RegisterEntry([FromBody] Vehicle vehicle)
        {
            var userId = HttpContext.Items["UserId"];

            if (vehicle == null)
            {
                return BadRequest("El objeto Vehicle no puede ser nulo");
            }

            try
            {
                // Llama al servicio para registrar la entrada del vehículo
                int parkingRecordId = _parkingService.RegisterEntry(vehicle);

                // Puedes devolver el ID del registro de estacionamiento u otra información según tus necesidades
                return Ok(new { ParkingRecordId = parkingRecordId });
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción y devuelve una respuesta de error
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("register-exit")]
        public IActionResult RegisterExit(int parkingRecordId, [FromBody] PurchaseRecord purchaseRecord)
        {
            try
            {
                // Llama al servicio para registrar la salida del vehículo
                decimal cost = _parkingService.RegisterExit(parkingRecordId, purchaseRecord);

                // Puedes devolver el costo o cualquier otra información según tus necesidades
                return Ok(new { Cost = cost });
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                // Maneja la excepción específica para el caso donde el registro de estacionamiento no se encuentra
                return NotFound($"Registro de estacionamiento con ID {parkingRecordId} no encontrado");
            }
            catch (Exception ex)
            {
                // Maneja cualquier otra excepción y devuelve una respuesta de error
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("parking-history")]
        public IActionResult GetParkingHistory(DateTime startTime, DateTime endTime)
        {
            try
            {
                // Llama al servicio para obtener el historial de estacionamiento
                IEnumerable<ParkingRecord> parkingHistory = _parkingService.GetParkingHistory(startTime, endTime);

                // Puedes devolver el historial de estacionamiento u otra información según tus necesidades
                return Ok(parkingHistory);
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción y devuelve una respuesta de error
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
