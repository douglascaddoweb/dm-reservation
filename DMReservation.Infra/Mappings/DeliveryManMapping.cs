using DMReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMReservation.Infra.Mappings
{
    public class DeliveryManMapping : IEntityTypeConfiguration<DeliveryMan>
    {
        public void Configure(EntityTypeBuilder<DeliveryMan> builder)
        {
            builder.ToTable("deliveryman");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasColumnType("int");
            builder.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar(100)");
            builder.Property(x => x.BirthDate).HasColumnName("birthdate").HasColumnType("timestamp with time zone");
            builder.Property(x => x.Image).HasColumnName("image").HasColumnType("varchar(50)");

            builder.ComplexProperty(x => x.Cnh, t =>
            {
                t.Property(x => x.Value).HasColumnName("cnh").HasColumnType("varchar(20)");
                t.Ignore(x => x.IsValid);
            });

            builder.ComplexProperty(x => x.TypeCnh, t =>
            {
                t.Property(x => x.Value).HasColumnName("typecnh").HasColumnType("varchar(2)");
                t.Ignore(x => x.IsValid);
            });

            builder.ComplexProperty(x => x.Cnpj, t =>
            {
                t.Property(x => x.Value).HasColumnName("cnpj").HasColumnType("varchar(20)");
                t.Ignore(x => x.IsValid);
            });

            builder.HasMany(d => d.Rental).WithOne(d => d.DeliveryMan).HasForeignKey(f => f.DeliveryManId);
            builder.HasMany(d => d.OrderDeliveryMan).WithOne(d => d.DeliveryMan).HasForeignKey(f => f.DeliveryManId);
        }
    }
}
