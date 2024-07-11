using Microsoft.EntityFrameworkCore;
using SigmaCandidate.Core.Models;
using System.Reflection;

namespace SigmaCandidate.Infrastructure.Data
{
    public class SigmaCandidateDbContext(DbContextOptions<SigmaCandidateDbContext> options) : DbContext(options)
    {
        public DbSet<CandidateModel> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateModel>(entity =>
            {
                entity.HasKey(e => e.Email);
            });

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}