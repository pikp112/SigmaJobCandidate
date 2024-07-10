namespace SigmaCandidate.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICandidateRepository Candidates { get; }

        Task SaveChangesAsync();
    }
}