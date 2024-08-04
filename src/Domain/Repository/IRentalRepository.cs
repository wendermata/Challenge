using Domain.Entities;

namespace Domain.Repository
{
    public interface IRentalRepository : IRepository<Rental>
    {
        Task<Rental> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Rental>> GetRentalsByMotorcycleId(Guid motorcycleId, CancellationToken cancellationToken);
    }
}
