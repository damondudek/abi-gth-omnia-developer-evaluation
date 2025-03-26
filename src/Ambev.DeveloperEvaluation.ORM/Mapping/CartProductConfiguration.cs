using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
{
    public void Configure(EntityTypeBuilder<CartProduct> builder)
    {
        builder.ToTable("CartProducts");

        builder.HasKey(cp => cp.Id);
        builder.Property(cp => cp.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(cp => cp.ProductId).IsRequired();
        builder.Property(cp => cp.Quantity).IsRequired();

        builder
            .HasOne<Cart>()
            .WithMany(c => c.Products)
            .HasForeignKey(cp => cp.CartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}