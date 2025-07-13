namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Configurations;

public class ShopConfiguration : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.ToTable("Shop");

        builder.Property(_ => _.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(_ => _.Email).HasMaxLength(255);

        builder.Property(_ => _.PhoneNumber).HasMaxLength(20);

        builder.Property(_ => _.AddressLine).HasMaxLength(255);

        builder.Property(_ => _.City).HasMaxLength(100);

        builder.Property(_ => _.Country).HasMaxLength(100);

        builder.HasOne(_ => _.Owner)
            .WithMany(u => u.Shops)
            .HasForeignKey(_ => _.OwnerId);

        builder.HasMany(_ => _.Products)
            .WithOne(p => p.Shop)
            .HasForeignKey(p => p.ShopId);
    }
}