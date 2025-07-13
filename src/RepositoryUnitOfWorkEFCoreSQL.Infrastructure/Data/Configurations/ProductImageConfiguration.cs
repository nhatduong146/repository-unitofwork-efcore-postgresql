namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImage");

        builder.Property(_ => _.ImageUrl).IsRequired();

        builder.HasOne(_ => _.Product)
            .WithMany(p => p.Images)
            .HasForeignKey(_ => _.ProductId);
    }
}