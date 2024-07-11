using Moq;
using SigmaCandidate.Infrastructure.Data;
using SigmaCandidate.Infrastructure.Repositories.Implementations;
using SigmaCandidate.Infrastructure.Repositories.Interfaces;

namespace SigmaCandidate.Infrastructure.Tests
{
    public class CandidateRepositoryTests
    {
        private readonly Mock<SigmaCandidateDbContext> _dbContext;
        private readonly ICandidateRepository _sut;

        public CandidateRepositoryTests()
        {
            _dbContext = new Mock<SigmaCandidateDbContext>();

            _sut = new CandidateRepository(_dbContext.Object);
        }
    }
}