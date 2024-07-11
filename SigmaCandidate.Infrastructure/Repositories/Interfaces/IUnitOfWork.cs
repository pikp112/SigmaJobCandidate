namespace SigmaCandidate.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICandidateRepository CandidateRepository { get; }

        Task SaveChangesAsync();
    }
}