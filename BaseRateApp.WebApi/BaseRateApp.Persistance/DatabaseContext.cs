using BaseRateApp.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseRateApp.Persistance
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(customer =>
            {
                customer.HasKey(m => m.Id);
                customer.Property(m => m.FirstName).IsRequired();
                customer.Property(m => m.LastName).IsRequired();
                customer.Property(m => m.PersonalId).IsRequired();
            });

            modelBuilder.Entity<Agreement>(agreement =>
            {
                agreement.HasKey(m => m.Id);

                agreement.Property(m => m.CustomerId).IsRequired();
                agreement.HasOne(m => m.Customer)
                    .WithMany(m => m.Agreements)
                    .HasForeignKey(m => m.CustomerId);

                agreement.Property(m => m.AgreementDuration).IsRequired();
                agreement.Property(m => m.Margin).IsRequired();
                agreement.Property(m => m.Margin).HasColumnType("decimal(4,2)");
                agreement.Property(m => m.Amount).IsRequired();
                agreement.Property(m => m.Amount).HasColumnType("decimal(14,2)");
                agreement.Property(m => m.BaseRateCode).IsRequired(); 
            });
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
    }
}
