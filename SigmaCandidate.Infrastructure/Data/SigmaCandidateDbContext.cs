using Microsoft.EntityFrameworkCore;
using SigmaCandidate.Core.Models;
using System.Reflection;

namespace SigmaCandidate.Infrastructure.Data
{
    public class SigmaCandidateDbContext : DbContext
    {
        public SigmaCandidateDbContext(DbContextOptions<SigmaCandidateDbContext> options)
            : base(options)
        {
        }

        public DbSet<CandidateModel> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateModel>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Email)
                      .IsUnique();
            });

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}