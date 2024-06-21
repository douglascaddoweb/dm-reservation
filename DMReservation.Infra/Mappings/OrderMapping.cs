using DMReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMReservation.Infra.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("order");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id).HasColumnName("id").HasColumnType("integer");
            builder.Property(d => d.CreatedAt).HasColumnName("createdat").HasColumnType("timestamp");
            builder.Property(d => d.Price).HasColumnName("price").HasColumnType("numeric(18,2)");
            builder.Property(d => d.Status).HasColumnName("status").HasColumnType("short");

            builder.HasOne(d => d.OrderDeliveryMan).WithOne(d => d.Order).HasForeignKey<OrderDeliveryMan>(f => f.OrderId);
        }
    }
}
