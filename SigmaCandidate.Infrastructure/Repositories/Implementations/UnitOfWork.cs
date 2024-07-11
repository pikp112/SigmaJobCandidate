using SigmaCandidate.Infrastructure.Data;
using SigmaCandidate.Infrastructure.Repositories.Interfaces;

namespace SigmaCandidate.Infrastructure.Repositories.Implementations
{
    public class UnitOfWork(SigmaCandidateDbContext context) : IUnitOfWork
    {
        private readonly SigmaCandidateDbContext _context = context;
        private ICandidateRepository _candidates;

        public ICandidateRepository CandidateRepository
        {
            get { return _candidates ??= new CandidateRepository(_context); }
        }

        public void Dispose() => _context.Dispose();

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}