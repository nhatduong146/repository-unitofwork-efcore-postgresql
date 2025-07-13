using RepositoryUnitOfWorkEFCoreSQL.Domain.Common;

namespace RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public ICollection<Review> Reviews { get; set; }
    public ICollection<Shop> Shops { get; set; }
}
