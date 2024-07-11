using Microsoft.EntityFrameworkCore;
using SigmaCandidate.Core.Models;
using SigmaCandidate.Infrastructure.Data;
using SigmaCandidate.Infrastructure.Repositories.Interfaces;
using SigmaCandidate.Infrastructure.Services;

namespace SigmaCandidate.Infrastructure.Repositories.Implementations
{
    public class CandidateRepository(SigmaCandidateDbContext context, ICacheService cacheService) : GenericRepository<CandidateModel>(context), ICandidateRepository
    {
        private readonly SigmaCandidateDbContext _context = context;
        private readonly ICacheService _cacheService = cacheService;

        public async Task<CandidateModel> GetCandidateByEmailAsync(string email)
        {
            var candidate = await _cacheService.GetOrCreateAsync(email, async () =>
            {
                return await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
            }, TimeSpan.FromMinutes(10));

            return candidate;
        }

        public async Task<IEnumerable<string>> GetExistingEmailsAsync(IEnumerable<string> emails)
        {
            return await _context.Candidates.Where(c => emails.Contains(c.Email)).Select(c => c.Email).ToListAsync();
        }

        Task<CandidateModel> ICandidateRepository.GetCandidateByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}