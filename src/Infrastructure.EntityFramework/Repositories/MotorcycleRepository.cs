using Application.Exceptions;
using Domain.Entities;
using Domain.Repository;
using Domain.Repository.Shared.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly ChallengeDbContext _context;
        private DbSet<Motorcycle> _motorcycles => _context.Set<Motorcycle>();

        public MotorcycleRepository(ChallengeDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfExistsAsync(string plate, CancellationToken cancellationToken)
        {
            return _motorcycles.Any(x => x.Plate.ToUpper().Equals(plate.ToUpper()));
        }

        public async Task DeleteAsync(Motorcycle entity, CancellationToken cancellationToken)
        {
            _motorcycles.Remove(entity);
        }

        public async Task<Motorcycle> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var motor = await _motorcycles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            NotFoundException.ThrowIfNull(motor, $"Motorcycle '{id}' not found.");
            return motor!;
        }

        public async Task InsertAsync(Motorcycle entity, CancellationToken cancellationToken)
        {
            await _motorcycles.AddAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(Motorcycle entity, CancellationToken cancellationToken)
        {
            _motorcycles.Update(entity);
        }

        public async Task<SearchOutput<Motorcycle>> Search(SearchInput input, CancellationToken cancellationToken)
        {
            var toSkip = (input.Page - 1) * input.PageSize;
            var query = _motorcycles.AsNoTracking().Where(x => x.IsActive);

            query = GetQuery(query, input.OrderBy, input.Order);

            if (!String.IsNullOrWhiteSpace(input.Search))
                query = query.Where(x => x.Plate.ToUpper().Contains(input.Search.ToUpper()));

            var list = await query.Skip(toSkip).Take(input.PageSize).ToListAsync(cancellationToken);
            var total = await query.CountAsync(cancellationToken);

            return new SearchOutput<Motorcycle>(input.Page, input.PageSize, total, list);
        }

        private IQueryable<Motorcycle> GetQuery(IQueryable<Motorcycle> query, string orderProperty, SearchOrder order)
        {
            var orderedQuery = (orderProperty.ToLower(), order) switch
            {
                ("model", SearchOrder.Asc) => query.OrderBy(x => x.Model),
                ("model", SearchOrder.Desc) => query.OrderByDescending(x => x.Model),
                ("year", SearchOrder.Asc) => query.OrderBy(x => x.Year),
                ("year", SearchOrder.Desc) => query.OrderByDescending(x => x.Year),
                ("plate", SearchOrder.Asc) => query.OrderBy(x => x.Model),
                ("plate", SearchOrder.Desc) => query.OrderByDescending(x => x.Plate),
                ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
                ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),

                _ => query.OrderBy(x => x.CreatedAt).ThenBy(x => x.Id)
            };

            return orderedQuery;
        }
    }
}
