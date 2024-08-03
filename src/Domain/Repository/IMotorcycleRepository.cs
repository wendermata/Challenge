using Domain.Entities;

namespace Domain.Repository
{
    public interface IMotorcycleRepository : IRepository<Motorcycle>
    {
        Task<Motorcycle> GetMotorcycleAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistsMotorcycleAsync(string plate, CancellationToken cancellationToken);
    }
}
