using Domain.Entities;
using Domain.Repository;
using Infra.Mongo.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infra.Mongo.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ChallengeDatabaseSettings _settings;
        private readonly IMongoCollection<Rental> _collection;

        public RentalRepository(IMongoService service, IOptions<ChallengeDatabaseSettings> settings)
        {
            _settings = settings.Value;
            _collection = service.Database.GetCollection<Rental>(_settings.RentalCollectionName);
        }

        public async Task DeleteAsync(Rental entity, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(c => c.Id == entity.Id, cancellationToken);
        }

        public async Task<Rental> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(c => c.Id == id, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Rental>> GetRentalsByMotorcycleId(Guid motorcycleId, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(c => c.MotorcycleId == motorcycleId, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }

        public async Task<List<Rental>> GetRentalsByRenterId(Guid renterId, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(c => c.RenterId == renterId, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }

        public async Task InsertAsync(Rental entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(Rental entity, CancellationToken cancellationToken)
        {
            await _collection.UpdateOneAsync(c => c.Id == entity.Id, Builders<Rental>.Update.Set(x => x, entity), cancellationToken: cancellationToken);
        }
    }
}
