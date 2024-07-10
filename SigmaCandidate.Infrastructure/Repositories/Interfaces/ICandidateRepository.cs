using SigmaCandidate.Core.Models;

namespace SigmaCandidate.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateRepository
    {
        Task<CandidateModel> GetCandidateByEmailAsync(string email);

        Task AddCandidateAsync(CandidateModel candidate);

        Task UpdateCandidateAsync(CandidateModel candidate);

        Task SaveChangesAsync();
    }
}