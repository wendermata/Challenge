using Domain.Entities;
using Domain.Repository;
using Domain.Repository.Shared.SearchableRepository;
using Infra.Mongo.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infra.Mongo.Repositories
{
    public class KafkaMessageRepository : IKafkaMessageRepository
    {
        private readonly ChallengeDatabaseSettings _settings;
        private readonly IMongoCollection<KafkaMessage> _collection;

        public KafkaMessageRepository(IMongoService service, IOptions<ChallengeDatabaseSettings> settings)
        {
            _settings = settings.Value;
            _collection = service.Database.GetCollection<KafkaMessage>(_settings.KafkaMessagesCollectionName);
        }

        public async Task DeleteAsync(KafkaMessage entity, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(c => c.Id == entity.Id, cancellationToken);
        }

        public async Task<KafkaMessage> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(c => c.Id == id, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task InsertAsync(KafkaMessage entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(KafkaMessage entity, CancellationToken cancellationToken)
        {
            var filter = Builders<KafkaMessage>.Filter.Eq(c => c.Id, entity.Id);
            await _collection.FindOneAndReplaceAsync(filter, entity, cancellationToken: cancellationToken);
        }
    }
}
