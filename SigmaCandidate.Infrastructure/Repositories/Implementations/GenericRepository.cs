using SigmaCandidate.Core.Models;
using SigmaCandidate.Infrastructure.Data;
using SigmaCandidate.Infrastructure.Repositories.Interfaces;

namespace SigmaCandidate.Infrastructure.Repositories.Implementations
{
    public class GenericRepository<T>(SigmaCandidateDbContext context) : IGenericRepository<T> where T : BaseModel
    {
        private readonly SigmaCandidateDbContext _context = context;

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>()
                          .AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(string email, T Entity)
        {
            var ex_entity = await _context.Set<T>()
                                          .FindAsync(email);

            if (ex_entity != null)
            {
                _context.Update(ex_entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddRangeAsync(IEnumerable<T> entities) // add bulk operations
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
    }
}