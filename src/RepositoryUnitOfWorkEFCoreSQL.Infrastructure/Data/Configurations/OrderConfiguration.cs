namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        builder.Property(_ => _.OrderCode)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(_ => _.OrderDate)
            .IsRequired();

        builder.Property(_ => _.TotalAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(_ => _.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(_ => _.OrderItems)
            .WithOne(_ => _.Order)
            .HasForeignKey(_ => _.OrderId);
    }
}
