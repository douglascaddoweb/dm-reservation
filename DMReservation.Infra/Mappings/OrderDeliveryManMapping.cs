using DMReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMReservation.Infra.Mappings
{
    public class OrderDeliveryManMapping : IEntityTypeConfiguration<OrderDeliveryMan>
    {
        public void Configure(EntityTypeBuilder<OrderDeliveryMan> builder)
        {
            builder.ToTable("orderdeliveryman");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id).HasColumnName("id").HasColumnType("integer");
            builder.Property(d => d.DeliveryManId).HasColumnName("iddeliveryman").HasColumnType("integer");
            builder.Property(d => d.OrderId).HasColumnName("idorder").HasColumnType("integer");

            builder.HasOne(d => d.Order).WithOne(d => d.OrderDeliveryMan).HasForeignKey<OrderDeliveryMan>(f => f.OrderId);
            //builder.HasOne(d => d.DeliveryMan).WithMany().HasForeignKey(d => d.DeliveryManId);
        }
    }
}
