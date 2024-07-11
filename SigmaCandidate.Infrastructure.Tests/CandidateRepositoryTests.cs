using AutoFixture.AutoMoq;
using AutoFixture;
using Moq;
using SigmaCandidate.Infrastructure.Data;
using SigmaCandidate.Infrastructure.Repositories.Implementations;
using SigmaCandidate.Infrastructure.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SigmaCandidate.Core.Models;
using Xunit;
using System.Linq.Expressions;

namespace SigmaCandidate.Infrastructure.Tests
{
    public class CandidateRepositoryTests
    {
        private readonly Mock<SigmaCandidateDbContext> _dbContextMock;
        private readonly CandidateRepository _candidateRepository;
        private readonly Fixture _fixture;

        public CandidateRepositoryTests()
        {
            _fixture = new Fixture();
            _dbContextMock = new Mock<SigmaCandidateDbContext>();
            _candidateRepository = new CandidateRepository(_dbContextMock.Object);
        }

        [Fact]
        public async Task GetCandidateByEmailAsync_ShouldReturnCandidate()
        {
            var email = "test@example.com";
            var candidate = _fixture.Build<CandidateModel>()
                .With(x => x.Email, email)
                .Create();

            var candidates = new List<CandidateModel>
            {
                candidate
            }.AsQueryable();

            _dbContextMock.Setup(db => db.Set<CandidateModel>()).Returns(candidates.CreateMockDbSet().Object);

            var expectedCandidateModels = new List<CandidateModel>
            {
                candidate
            };

            //var mockDbSet = new Mock<DbSet<CandidateModel>>();
            //mockDbSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<CandidateModel, bool>>>(), default))
            //         .ReturnsAsync(expectedCandidate);
            //_dbContextMock.Setup(c => c.Candidates).Returns(mockDbSet.Object);

            var result = await _candidateRepository.GetCandidateByEmailAsync(email);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedCandidateModels);
        }
    }
}