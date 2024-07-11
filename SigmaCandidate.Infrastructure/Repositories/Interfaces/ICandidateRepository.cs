using SigmaCandidate.Core.Models;

namespace SigmaCandidate.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateRepository : IGenericRepository<CandidateModel>
    {
        Task<CandidateModel> GetCandidateByEmailAsync(string email);

        Task<IEnumerable<string>> GetExistingEmailsAsync(IEnumerable<string> emails);
    }
}