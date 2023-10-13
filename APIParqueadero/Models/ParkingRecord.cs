namespace APIParqueadero.Models
{
    public class ParkingRecord
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string? VehicleType { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public decimal Cost { get; set; }
        public int PurchaseRecordId { get; internal set; }
    }
}
