using SigmaCandidate.Infrastructure.Data;
using SigmaCandidate.Infrastructure.Repositories.Interfaces;
using SigmaCandidate.Infrastructure.Services;

namespace SigmaCandidate.Infrastructure.Repositories.Implementations
{
    public class UnitOfWork(SigmaCandidateDbContext context, ICacheService cacheService) : IUnitOfWork
    {
        private readonly SigmaCandidateDbContext _context = context;
        private readonly ICacheService _cacheService;
        private ICandidateRepository _candidates;

        public ICandidateRepository CandidateRepository
        {
            get { return _candidates ??= new CandidateRepository(_context, _cacheService); }
        }

        public void Dispose() => _context.Dispose();

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}