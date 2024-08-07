using Domain.Entities;

namespace Domain.Repository
{
    public interface IRenterRepository : IRepository<Renter>
    {
        Task<Renter> GetByDocumentAsync(string document, CancellationToken cancellationToken);
        Task<Renter> GetByLicenseAsync(string licenseNumber, CancellationToken cancellationToken);
    }
}
