namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");

        builder.Property(_ => _.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasMany(_ => _.Products)
            .WithOne(_ => _.Category)
            .HasForeignKey(_ => _.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
