using DMReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMReservation.Infra.Mappings
{
    public class MotorcycleMapping : IEntityTypeConfiguration<Motorcycle>
    {
        public void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            builder.ToTable("motorcycle");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasColumnType("integer");
            builder.Property(x => x.Year).HasColumnName("year").HasColumnType("smallint");
            builder.Property(x => x.Model).HasColumnName("model").HasColumnType("varchar(50)");
            builder.ComplexProperty(x => x.LicensePlate, t =>
            {
                t.Property(x => x.Value).HasColumnName("licenseplate").HasColumnType("varchar(10)");
                t.Ignore(x => x.IsValid);
            });

            builder.HasMany(w => w.Rentals).WithOne(w => w.Motorcycle).HasForeignKey(f => f.MotorcycleId);
        }
    }
}
