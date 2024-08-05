using Application.Interfaces;

namespace Infrastructure.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChallengeDbContext _context;

        public UnitOfWork(ChallengeDbContext context)
        {
            _context = context;
        }

        public async Task Commit(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);
        public Task Rollback(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
