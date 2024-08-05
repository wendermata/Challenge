using Application.Exceptions;
using Domain.Entities;
using Domain.Repository;
using Domain.Repository.Shared.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ChallengeDbContext _context;
        private DbSet<Rental> _rentals => _context.Set<Rental>();

        public RentalRepository(ChallengeDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(Rental entity, CancellationToken cancellationToken)
        {
            _rentals.Remove(entity);
        }

        public async Task<Rental> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var rental = await _rentals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            NotFoundException.ThrowIfNull(rental, $"Rental '{id}' not found.");
            return rental!;
        }

        public async Task<List<Rental>> GetRentalsByMotorcycleId(Guid motorcycleId, CancellationToken cancellationToken)
        {
            var rentals = await _rentals.Where(x => x.MotorcycleId.Equals(motorcycleId)).ToListAsync();
            NotFoundException.ThrowIfNull(rentals, $"Rentals not found for motorcycle '{motorcycleId}'.");
            return rentals!;
        }
        public async Task<List<Rental>> GetRentalsByRenterId(Guid renterId, CancellationToken cancellationToken)
        {
            var rentals = await _rentals.Where(x => x.RenterId.Equals(renterId)).ToListAsync();
            NotFoundException.ThrowIfNull(rentals, $"Rentals not found for renter '{renterId}'.");
            return rentals!;
        }

        public async Task InsertAsync(Rental entity, CancellationToken cancellationToken)
        {
            await _rentals.AddAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(Rental entity, CancellationToken cancellationToken)
        {
            _rentals.Update(entity);
        }

        private IQueryable<Rental> GetQuery(IQueryable<Rental> query, string orderProperty, SearchOrder order)
        {
            var orderedQuery = (orderProperty.ToLower(), order) switch
            {
                ("initialdate", SearchOrder.Asc) => query.OrderBy(x => x.InitialDate),
                ("initialdate", SearchOrder.Desc) => query.OrderByDescending(x => x.InitialDate),
                ("expecteddevolutiondate", SearchOrder.Asc) => query.OrderBy(x => x.ExpectedDevolutionDate),
                ("expecteddevolutiondate", SearchOrder.Desc) => query.OrderByDescending(x => x.ExpectedDevolutionDate),
                ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
                ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
                _ => query.OrderBy(x => x.CreatedAt).ThenBy(x => x.Id)
            };

            return orderedQuery;
        }
    }
}
