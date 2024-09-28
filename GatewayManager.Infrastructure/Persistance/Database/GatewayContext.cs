using Microsoft.EntityFrameworkCore;
using GatewayManager.Domain.Entities;

namespace GatewayManager.Infrastructure.Persistance.Database
{
    public class GatewayContext: DbContext
    {
        public GatewayContext(DbContextOptions<GatewayContext> options) : base(options)
        {}

        #region DbSets
        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<PeripheralDevice> PeripheralDevices { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gateway>()
           .HasIndex(g => g.SerialNumber)
           .IsUnique();

            modelBuilder.Entity<Gateway>()
                .HasMany(g => g.PeripheralDevices)
                .WithOne(p => p.Gateway)
                .HasForeignKey(p => p.GatewayId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PeripheralDevice>()
                .Property(p => p.Status)
                .HasConversion<string>();
        }
    }
}
