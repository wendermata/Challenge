using Domain.Entities;
using Domain.Repository;

namespace Infrastructure.EntityFramework.Repositories
{
    public class RenterRepository : IRenterRepository
    {
        public Task DeleteAsync(Renter entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Renter entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Renter entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
