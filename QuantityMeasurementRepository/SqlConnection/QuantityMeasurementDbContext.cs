using Microsoft.EntityFrameworkCore;
using QuantityMeasurementModel;
namespace QuantityMeasurementRepository
{
    public class QuantityMeasurementDbContext : DbContext
    {
        public QuantityMeasurementDbContext(DbContextOptions<QuantityMeasurementDbContext> options) : base(options)
        {
        }
        public DbSet<QuantityMeasurementEntity> QuantityMeasurements { get; set; }
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<QuantityMeasurementEntity>()
            .Property(e => e.MeasurementAction)
            .HasConversion<string>(); 
            modelBuilder.Entity<QuantityMeasurementEntity>()
            .ToTable("MeasurementDataHistory", tb => tb.HasTrigger("spInsertMeasurement")); 
        }
    }
}