using Domain.Entities;

namespace Domain.Repository
{
    public interface IMotorcycleRepository : IRepository<Motorcycle>
    {
        Task<Motorcycle> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> CheckIfExistsAsync(string plate, CancellationToken cancellationToken);
        Task<List<Motorcycle>> FilterByPlateAsync(string queryPlate, CancellationToken cancellationToken);
    }
}
