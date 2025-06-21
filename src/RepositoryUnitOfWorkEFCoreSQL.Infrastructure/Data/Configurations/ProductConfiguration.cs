namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.Property(_ => _.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasOne(_ => _.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(_ => _.CategoryId);

        builder.HasIndex(_ => _.Name);
    }
}