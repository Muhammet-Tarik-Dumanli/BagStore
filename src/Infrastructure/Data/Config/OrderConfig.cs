using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.BuyerId)
                .IsRequired();

            builder.OwnsOne(x => x.ShippingAddress, a =>
            {
                a.WithOwner();

                a.Property(a => a.Street)
                    .IsRequired()
                    .HasMaxLength(180);
                a.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(100);
                a.Property(a => a.State)
                    .HasMaxLength(60);
                a.Property(a => a.Country)
                    .IsRequired()
                    .HasMaxLength(90);
                a.Property(a => a.ZipCode)
                    .IsRequired()
                    .HasMaxLength(18);
            });

            builder.Navigation(x => x.ShippingAddress)
                .IsRequired();
        }
    }
}
