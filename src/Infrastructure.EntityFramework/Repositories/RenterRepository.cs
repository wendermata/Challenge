using Application.Exceptions;
using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories
{
    public class RenterRepository : IRenterRepository
    {
        private readonly ChallengeDbContext _context;
        private DbSet<Renter> _renters => _context.Set<Renter>();

        public RenterRepository(ChallengeDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(Renter entity, CancellationToken cancellationToken)
        {
            _renters.Remove(entity);
        }

        public async Task InsertAsync(Renter entity, CancellationToken cancellationToken)
        {
            await _renters.AddAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(Renter entity, CancellationToken cancellationToken)
        {
            _renters.Update(entity);
        }

        public async Task<Renter> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var renter = await _renters.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            NotFoundException.ThrowIfNull(renter, $"Renter '{id}' not found.");
            return renter!;
        }

        public async Task<Renter> GetByDocumentAsync(string document, CancellationToken cancellationToken)
        {
            return await _renters.AsNoTracking().FirstOrDefaultAsync(x => x.Document == document, cancellationToken);
        }

        public async Task<Renter> GetByLicenseAsync(string licenseNumber, CancellationToken cancellationToken)
        {
            return await _renters.AsNoTracking().FirstOrDefaultAsync(x => x.LicenseNumber == licenseNumber, cancellationToken);
        }
    }
}
