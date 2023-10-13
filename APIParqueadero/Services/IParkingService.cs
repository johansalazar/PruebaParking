using APIParqueadero.Models;
using Microsoft.Extensions.Hosting;

namespace APIParqueadero.Services
{
    public interface IParkingService
    {
        // Registrar la entrada de un vehículo y devolver el ID del registro de estacionamiento
        int RegisterEntry(Vehicle vehicle);

        // Registrar la salida de un vehículo.
        decimal RegisterExit(int parkingRecordId, PurchaseRecord purchaseRecord);

        // Obtener el historial de estacionamiento en un rango de tiempo
        IEnumerable<ParkingRecord> GetParkingHistory(DateTime startTime, DateTime endTime);

        //calcular el costo y aplicar descuentos si es necesario
        decimal CalculateParkingCost(DateTime startTime, DateTime endTime, string vehicleType);
        ParkingRecord GetParkingRecordById(int parkingRecordId);
        void UpdateParkingRecord(ParkingRecord parkingRecord);
    }
}
