using SigmaCandidate.Core.Models;
using SigmaCandidate.Infrastructure.Data;
using SigmaCandidate.Infrastructure.Repositories.Interfaces;

namespace SigmaCandidate.Infrastructure.Repositories.Implementations
{
    public class CandidateRepository(SigmaCandidateDbContext context) : ICandidateRepository
    {
        private readonly SigmaCandidateDbContext _context = context;

        public async Task AddCandidateAsync(CandidateModel candidate)
        {
            throw new NotImplementedException();
        }

        public async Task<CandidateModel> GetCandidateByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCandidateAsync(CandidateModel candidate)
        {
            throw new NotImplementedException();
        }
    }
}