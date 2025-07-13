namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Review");

        builder.Property(_ => _.Rating).IsRequired();

        builder.Property(_ => _.Comment).HasMaxLength(1000);

        builder.HasOne(_ => _.Product)
            .WithMany(p => p.Reviews)   
            .HasForeignKey(_ => _.ProductId);

        builder.HasOne(_ => _.Customer)
            .WithMany(u => u.Reviews)
            .HasForeignKey(_ => _.CustomerId);
    }
}