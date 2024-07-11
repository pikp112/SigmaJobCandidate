using SigmaCandidate.API.Controllers;
using SigmaCandidate.Core.Models;
using SigmaCandidate.Infrastructure.Data;
using Xunit;

namespace SigmaCandidate.API.Tests
{
    public class CandidatesControllerTests
    {
        [Fact]
        public async Task CreateOrUpdateCandidate_CreatesCandidate()
        {
            var options = new DbContextOptionsBuilder<SigmaCandidateDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var context = new SigmaCandidateDbContext(options);
            var controller = new CandidatesController(context);

            var candidate = new CandidateModel
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123456789",
                CallTimeInterval = "9-5",
                LinkedInProfileUrl = "http://linkedin.com",
                GitHubProfileUrl = "http://github.com",
                Comment = "Test comment"
            };

            var result = await controller.CreateCandidate(candidate);

            Assert.AreEqual(1, context.Candidates.Count());
        }
    }
}