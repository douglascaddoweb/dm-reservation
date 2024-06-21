using DMReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMReservation.Infra.Mappings
{
    public class RentalMapping : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("rental");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasColumnType("int4");
            builder.Property(x => x.DeliveryManId).HasColumnName("iddeliveryman").HasColumnType("int4");
            builder.Property(x => x.MotorcycleId).HasColumnName("idmotorcycle").HasColumnType("int4");
            builder.Property(x => x.RentalPlanId).HasColumnName("idrentalplan").HasColumnType("int2");
            builder.Property(x => x.DateStart).HasColumnName("datestart").HasColumnType("timestamp");
            builder.Property(x => x.DateFinish).HasColumnName("datefinish").HasColumnType("timestamp");
            builder.Property(x => x.DateForecastFinish).HasColumnName("dateforecastfinish").HasColumnType("timestamp");
            builder.Property(x => x.CreatedAt).HasColumnName("createdat").HasColumnType("timestamp");
            builder.Property(x => x.Status).HasColumnName("status").HasColumnType("int2");
            builder.Property(x => x.Price).HasColumnName("price").HasColumnType("numeric(18,2)");
            builder.Property(x => x.Fine).HasColumnName("fine").HasColumnType("numeric(5,2)");
            builder.Property(x => x.ExtraPrice).HasColumnName("extraprice").HasColumnType("numeric(18,2)");
            builder.Property(x => x.Total).HasColumnName("total").HasColumnType("numeric(18,2)");

            builder.HasOne(x => x.Motorcycle).WithMany(f => f.Rentals).HasForeignKey(f => f.MotorcycleId);
            builder.HasOne(x => x.DeliveryMan).WithMany(f => f.Rental).HasForeignKey(f => f.DeliveryManId);
            builder.HasOne(x => x.RentalPlan).WithMany().HasForeignKey(f => f.RentalPlanId);
        }
    }
}
