using DMReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMReservation.Infra.Mappings
{
    public class NotifyOrderMapping : IEntityTypeConfiguration<NotifyOrder>
    {
        public void Configure(EntityTypeBuilder<NotifyOrder> builder)
        {
            builder.ToTable("notifyorder");
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Id).HasColumnName("id").HasColumnType("integer");
            builder.Property(d => d.IdDeliveryMan).HasColumnName("iddeliveryman").HasColumnType("integer");
            builder.Property(d => d.IdOrder).HasColumnName("idorder").HasColumnType("integer");
            builder.Property(d => d.CreatedAt).HasColumnName("createdat").HasColumnType("timestamp");

            builder.HasOne(d => d.Order).WithMany().HasForeignKey(f => f.IdOrder);
            builder.HasOne(d => d.DeliveryMan).WithMany().HasForeignKey(f => f.IdDeliveryMan);
        }
    }
}
