using Domain.Entities;
using Domain.Repository;
using Infra.Mongo.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infra.Mongo.Repositories
{
    public class RenterRepository : IRenterRepository
    {
        private readonly ChallengeDatabaseSettings _settings;
        private readonly IMongoCollection<Renter> _collection;

        public RenterRepository(IMongoService service, IOptions<ChallengeDatabaseSettings> settings)
        {
            _settings = settings.Value;
            _collection = service.Database.GetCollection<Renter>(_settings.RenterCollectionName);
        }

        public async Task DeleteAsync(Renter entity, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(c => c.Id == entity.Id, cancellationToken);
        }

        public async Task<Renter> GetByDocumentAsync(string document, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(c => c.Document == document, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Renter> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(c => c.Id == id, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Renter> GetByLicenseAsync(string licenseNumber, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(c => c.LicenseNumber == licenseNumber, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task InsertAsync(Renter entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(Renter entity, CancellationToken cancellationToken)
        {
            await _collection.UpdateOneAsync(c => c.Id == entity.Id, Builders<Renter>.Update.Set(x => x, entity), cancellationToken: cancellationToken);
        }
    }
}
