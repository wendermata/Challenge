using Domain.Entities;
using Domain.Repository;
using Domain.Repository.Shared.SearchableRepository;
using Infra.Mongo.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infra.Mongo.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly ChallengeDatabaseSettings _settings;
        private readonly IMongoCollection<Motorcycle> _collection;

        public MotorcycleRepository(IMongoService service, IOptions<ChallengeDatabaseSettings> settings)
        {
            _settings = settings.Value;
            _collection = service.Database.GetCollection<Motorcycle>(_settings.MotorcycleCollectionName);
        }

        public async Task<bool> CheckIfExistsAsync(string plate, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(c => c.Plate == plate, cancellationToken: cancellationToken);
            return await cursor.AnyAsync(cancellationToken);
        }

        public async Task<Motorcycle> GetMotorcycleByPlateAsync(string plate, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(c => c.Plate == plate, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task DeleteAsync(Motorcycle entity, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(c => c.Id == entity.Id, cancellationToken);
        }

        public async Task<Motorcycle> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(c => c.Id == id, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task InsertAsync(Motorcycle entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task<SearchOutput<Motorcycle>> Search(SearchInput input, CancellationToken cancellationToken)
        {
            var toSkip = (input.Page - 1) * input.PageSize;
            var filterBuilder = Builders<Motorcycle>.Filter;
            var filter = filterBuilder.Eq(x => x.IsActive, true);

            if (!string.IsNullOrWhiteSpace(input.Search))
            {
                var searchFilter = filterBuilder.Regex(x => x.Plate, new MongoDB.Bson.BsonRegularExpression(input.Search, "i"));
                filter = filterBuilder.And(filter, searchFilter);
            }

            var sortBuilder = Builders<Motorcycle>.Sort;
            var sort = (input.OrderBy.ToLower(), input.Order) switch
            {
                ("model", SearchOrder.Asc) => sortBuilder.Ascending(x => x.Model),
                ("model", SearchOrder.Desc) => sortBuilder.Descending(x => x.Model),
                ("year", SearchOrder.Asc) => sortBuilder.Ascending(x => x.Year),
                ("year", SearchOrder.Desc) => sortBuilder.Descending(x => x.Year),
                ("plate", SearchOrder.Asc) => sortBuilder.Ascending(x => x.Plate),
                ("plate", SearchOrder.Desc) => sortBuilder.Descending(x => x.Plate),
                ("createdat", SearchOrder.Asc) => sortBuilder.Ascending(x => x.CreatedAt),
                ("createdat", SearchOrder.Desc) => sortBuilder.Descending(x => x.CreatedAt),
                _ => sortBuilder.Ascending(x => x.CreatedAt).Ascending(x => x.Id),
            };

            var query = _collection.Find(filter).Sort(sort).Skip(toSkip).Limit(input.PageSize);

            var list = await query.ToListAsync(cancellationToken);
            var total = await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

            return new SearchOutput<Motorcycle>(input.Page, input.PageSize, (int)total, list);
        }

        public async Task UpdateAsync(Motorcycle entity, CancellationToken cancellationToken)
        {
            var filter = Builders<Motorcycle>.Filter.Eq(c => c.Id, entity.Id);
            var updateDefinition = new UpdateDefinitionBuilder<Motorcycle>();
            var updates = new List<UpdateDefinition<Motorcycle>>
            {
                updateDefinition.Set(e => e.Year, entity.Year),
                updateDefinition.Set(e => e.Model, entity.Model),
                updateDefinition.Set(e => e.Plate, entity.Model),
                updateDefinition.Set(e => e.IsActive, entity.IsActive),
                updateDefinition.Set(e => e.CreatedAt, entity.CreatedAt),
                updateDefinition.Set(e => e.UpdatedAt, entity.UpdatedAt)
            };

            var update = updateDefinition.Combine(updates);
            await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        }
    }
}
