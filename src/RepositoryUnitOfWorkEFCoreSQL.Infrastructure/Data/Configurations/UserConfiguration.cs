namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.Property(_ => _.FirstName).HasMaxLength(100);
        builder.Property(_ => _.LastName).HasMaxLength(100);
        builder.Property(_ => _.Email).IsRequired().HasMaxLength(255);
        builder.Property(_ => _.PhoneNumber).HasMaxLength(20);

        builder.HasMany(_ => _.Reviews)
            .WithOne(r => r.Customer)
            .HasForeignKey(r => r.CustomerId);

        builder.HasMany(_ => _.Shops)
            .WithOne(s => s.Owner)
            .HasForeignKey(s => s.OwnerId);
    }
}
