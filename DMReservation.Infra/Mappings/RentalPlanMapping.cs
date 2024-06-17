using DMReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMReservation.Infra.Mappings
{
    public class RentalPlanMapping : IEntityTypeConfiguration<RentalPlan>
    {
        public void Configure(EntityTypeBuilder<RentalPlan> builder)
        {
            builder.ToTable("rentalplan");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasColumnType("int2");
            builder.Property(x => x.Days).HasColumnName("days").HasColumnType("int2");
            builder.Property(x => x.Price).HasColumnName("price").HasColumnType("numeric(18,2)");
            builder.Property(x => x.Fine).HasColumnName("fine").HasColumnType("numeric(5,2)");
            builder.Property(x => x.ExtraPrice).HasColumnName("extraprice").HasColumnType("numeric(18,2)");

        }
    }
}
