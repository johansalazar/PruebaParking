namespace APIParqueadero.Models
{
    public class PurchaseRecord
    {
        public int Id { get; set; }
        public int ParkingRecordId { get; set; }
        public string? SupermarketInvoiceNumber { get; set; }
    }
}
