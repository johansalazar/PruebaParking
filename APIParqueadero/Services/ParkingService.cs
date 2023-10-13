using APIParqueadero.Models;
using System.Collections.Generic;

namespace APIParqueadero.Services
{
    public class ParkingService : IParkingService
    {
        private readonly IParkingService _parkingRepository;
        private readonly List<ParkingRecord> _parkingRecords = new List<ParkingRecord>();
        private int _nextParkingRecordId = 1;
        private readonly BaseDBContext _context;
        public ParkingService(IParkingService parkingRepository)
        {
            _parkingRepository = parkingRepository;
        }

        // Implementa métodos de servicios 
        public decimal CalculateParkingCost(DateTime startTime, DateTime endTime, string vehicleType)
        {
            // Calcula la duración del estacionamiento en minutos
            TimeSpan duration = endTime - startTime;
            int totalMinutes = (int)duration.TotalMinutes;

            // Aplica la tarifa correspondiente al tipo de vehículo
            decimal rate = GetRateForVehicleType(vehicleType);

            // Calcula el costo total
            decimal totalCost = totalMinutes * rate;

            return totalCost;
        }

        private decimal GetRateForVehicleType(string vehicleType)
        {
            // Retorna la tarifa correspondiente al tipo de vehículo
            switch (vehicleType.ToLower())
            {
                case "car":
                    return 110;
                case "motorcycle":
                    return 50;
                case "bicycle":
                    return 10;
                default:                   
                    throw new ArgumentException("Tipo de vehículo desconocido");
            }
        }

        public IEnumerable<ParkingRecord> GetParkingHistory(DateTime startTime, DateTime endTime)
        {           
            List<ParkingRecord> parkingRecords = GetParkingRecordsFromDatabase();

            var filteredRecords = parkingRecords
                .Where(record => record.EntryTime >= startTime && record.ExitTime <= endTime)
                .ToList();

            return filteredRecords;
        }

        private List<ParkingRecord> GetParkingRecordsFromDatabase()
        {           
            var records = _context.ParkingRecords.ToList();
            List<ParkingRecord> filteredRecords = new List<ParkingRecord>();
            foreach (var record in records)
            {
                ParkingRecord parkingRecord = new()
                {
                   
                    Id = record.Id,
                    VehicleId = record.VehicleId,
                    VehicleType = record.VehicleType,
                    Cost = record.Cost,
                    EntryTime = record.EntryTime,
                    ExitTime = record.ExitTime,
                    PurchaseRecordId = record.PurchaseRecordId
                };
                filteredRecords.Add(parkingRecord);
            }
            return filteredRecords;
        }


        public int RegisterEntry(Vehicle vehicle)
        {
            // Registra la entrada del vehículo y devuelve el ID del nuevo registro
            var parkingRecord = new ParkingRecord
            {
                Id = _nextParkingRecordId++,
                VehicleId = vehicle.Id, 
                EntryTime = DateTime.Now,

            };

            _parkingRecords.Add(parkingRecord);
            _context.ParkingRecords.Add(parkingRecord);

            return  parkingRecord.Id;
        }

        public decimal RegisterExit(int parkingRecordId, PurchaseRecord purchaseRecord)
        {
            // Obtén el registro de estacionamiento correspondiente al ID
            var parkingRecord = _parkingRepository.GetParkingRecordById(parkingRecordId);

            if (parkingRecord == null)
            {
                // Manejo de caso donde el registro de estacionamiento no se encuentra
                throw new InvalidOperationException("Registro de estacionamiento no encontrado");
            }

            // Actualiza la hora de salida y calcula el costo
            parkingRecord.ExitTime = DateTime.Now;
            parkingRecord.Cost = CalculateParkingCost(parkingRecord.EntryTime, parkingRecord.ExitTime, parkingRecord.VehicleType);

            // Si hay información de compra, aplica el descuento
            if (purchaseRecord != null)
            {
                ApplyDiscount(parkingRecord, purchaseRecord);
            }

            // Actualiza el registro de estacionamiento en el repositorio          
            _context.ParkingRecords.Update(parkingRecord);
            return parkingRecord.Cost;
        }

        private void ApplyDiscount(ParkingRecord parkingRecord, PurchaseRecord purchaseRecord)
        {
            // Aplica el descuento según las reglas de tu aplicación           
            decimal discountPercentage = 0.3m; // 30%
            decimal discountAmount = parkingRecord.Cost * discountPercentage;
            parkingRecord.Cost -= discountAmount;

            // Asigna el número de factura del supermercado al registro de compra
            parkingRecord.PurchaseRecordId = purchaseRecord.Id;
        }





        public ParkingRecord GetParkingRecordById(int parkingRecordId)
        {
            // Busca el registro de estacionamiento por ID en base de datos            
            return _context.ParkingRecords.FirstOrDefault(record => record.Id == parkingRecordId);
        }

        public void UpdateParkingRecord(ParkingRecord parkingRecord)
        {
            // Actualiza el registro de estacionamiento en en base de datos            
            var existingRecord = _context.ParkingRecords.FirstOrDefault(record => record.Id == parkingRecord.Id);

            if (existingRecord != null)
            {
                // Reemplaza el registro existente con la nueva información
                _context.ParkingRecords.Remove(existingRecord);
                _context.ParkingRecords.Add(parkingRecord);
            }
            else
            {
                // Manejo de caso donde el registro no se encuentra
                throw new InvalidOperationException("Registro de estacionamiento no encontrado para actualizar");
            }
        }
    }
}
