using Microsoft.EntityFrameworkCore;

namespace APIParqueadero.Models
{
    public class BaseDBContext : DbContext
    {
        public BaseDBContext(DbContextOptions<BaseDBContext> options)
            : base(options)
        {
        }

        public DbSet<ParkingRecord> ParkingRecords { get; set; } = null!;
        public DbSet<PurchaseRecord> PurchaseRecords { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;

    }
}
