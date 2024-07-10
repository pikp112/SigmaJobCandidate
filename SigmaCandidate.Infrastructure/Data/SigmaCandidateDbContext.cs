using Microsoft.EntityFrameworkCore;
using SigmaCandidate.Core.Models;

namespace SigmaCandidate.Infrastructure.Data
{
    public class SigmaCandidateDbContext(DbContextOptions<SigmaCandidateDbContext> options) : DbContext(options)
    {
        public DbSet<CandidateModel> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateModel>()
                .HasIndex(c => c.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}