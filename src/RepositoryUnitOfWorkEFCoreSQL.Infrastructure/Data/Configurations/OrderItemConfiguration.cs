namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem");

        builder.Property(_ => _.Quantity).IsRequired();

        builder.Property(_ => _.UnitPrice).HasColumnType("decimal(18,2)");

        builder.Property(_ => _.TotalPrice).HasColumnType("decimal(18,2)");

        builder.HasOne(_ => _.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(_ => _.ProductId);

        builder.HasOne(_ => _.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(_ => _.OrderId);
    }
}
