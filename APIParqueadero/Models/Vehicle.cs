namespace APIParqueadero.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? PlateNumber { get; set; }
        public decimal Cost { get; set; }
    }
}
