namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.Property(_ => _.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(_ => _.Description)
            .HasMaxLength(1000);

        builder.Property(_ => _.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(_ => _.IsActive)
            .IsRequired();

        builder.Property(_ => _.AverageRating);

        builder.Property(_ => _.TotalReviews);

        builder.HasIndex(_ => _.Name);

        builder.HasOne(_ => _.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(_ => _.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(_ => _.Shop)
            .WithMany(s => s.Products)
            .HasForeignKey(_ => _.ShopId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(_ => _.Reviews)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId);

        builder.HasMany(_ => _.Images)
            .WithOne(pi => pi.Product)
            .HasForeignKey(pi => pi.ProductId);

        builder.HasMany(_ => _.OrderItems)
            .WithOne(oi => oi.Product)
            .HasForeignKey(oi => oi.ProductId);
    }
}