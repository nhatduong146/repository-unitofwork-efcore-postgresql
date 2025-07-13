using RepositoryUnitOfWorkEFCoreSQL.Domain.Common;

namespace RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;

public class Shop : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public string AddressLine { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public string LogoUrl { get; set; }
    public bool IsVerified { get; set; }

    public Guid OwnerId { get; set; }
    public User Owner { get; set; }

    public ICollection<Product> Products { get; set; }

}
