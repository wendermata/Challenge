using Domain.Entities;
using Domain.Repository;
using Domain.Repository.Shared.SearchableRepository;

namespace Infrastructure.EntityFramework.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        public Task DeleteAsync(Rental entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Rental> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<Rental>> GetRentalsByMotorcycleId(Guid motorcycleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Rental entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<SearchOutput<Rental>> Search(SearchInput input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Rental entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
