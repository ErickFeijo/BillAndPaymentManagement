using RegisterServiceAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RegisterServiceAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Bill entity
            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("Bills");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd(); 
                entity.Property(e => e.BarCode).IsRequired(); 
                entity.Property(e => e.Expiration).IsRequired(); 
                entity.Property(e => e.Name).IsRequired(); 
                entity.Property(e => e.Situation).IsRequired(); 
            });

            // Configure the Payment entity
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.BarCode).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                
            });
        }
    }
}
