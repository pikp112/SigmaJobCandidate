using SigmaCandidate.Infrastructure.Data;
using SigmaCandidate.Infrastructure.Repositories.Interfaces;

namespace SigmaCandidate.Infrastructure.Repositories.Implementations
{
    public class UnitOfWork(SigmaCandidateDbContext context) : IUnitOfWork
    {
        private readonly SigmaCandidateDbContext _context = context;
        private ICandidateRepository _candidates;

        public ICandidateRepository Candidates
        {
            get { return _candidates ??= new CandidateRepository(_context); }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}