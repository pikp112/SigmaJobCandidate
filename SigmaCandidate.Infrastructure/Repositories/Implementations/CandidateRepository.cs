using Microsoft.EntityFrameworkCore;
using SigmaCandidate.Core.Models;
using SigmaCandidate.Infrastructure.Data;
using SigmaCandidate.Infrastructure.Repositories.Interfaces;

namespace SigmaCandidate.Infrastructure.Repositories.Implementations
{
    public class CandidateRepository(SigmaCandidateDbContext context) : GenericRepository<CandidateModel>(context), ICandidateRepository
    {
        private readonly SigmaCandidateDbContext _context = context;

        public async Task<CandidateModel> GetCandidateByEmailAsync(string email)
        {
            return await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}