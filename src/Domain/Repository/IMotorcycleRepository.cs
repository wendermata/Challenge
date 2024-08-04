using Domain.Entities;
using Domain.Repository.Shared.SearchableRepository;

namespace Domain.Repository
{
    public interface IMotorcycleRepository : IRepository<Motorcycle>, ISearchableRepository<Motorcycle>
    {
        Task<Motorcycle> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> CheckIfExistsAsync(string plate, CancellationToken cancellationToken);
    }
}