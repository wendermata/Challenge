using Domain.Entities;
using Domain.Repository.Shared.SearchableRepository;

namespace Domain.Repository
{
    public interface IRentalRepository : IRepository<Rental>, ISearchableRepository<Rental>
    {
        Task<Rental> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Rental>> GetRentalsByMotorcycleId(Guid motorcycleId, CancellationToken cancellationToken);
    }
}
