using SigmaCandidate.Core.Models;

namespace SigmaCandidate.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        Task AddAsync(T entity);

        Task UpdateAsync(string email, T Entity);

        Task AddRangeAsync(IEnumerable<T> entities);
    }
}