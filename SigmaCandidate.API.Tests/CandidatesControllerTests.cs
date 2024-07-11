using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Newtonsoft.Json;
using SigmaCandidate.API.Controllers;
using SigmaCandidate.Core.Dtos;
using SigmaCandidate.Core.Models;
using SigmaCandidate.Infrastructure.Data;
using SigmaCandidate.Infrastructure.Repositories.Interfaces;
using System.Net;
using System.Text;
using Xunit;

namespace SigmaCandidate.API.Tests
{
    public class CandidatesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CandidatesController _controller;
        private readonly Fixture _fixture;
        private readonly HttpClient _httpClient;

        public CandidatesControllerTests(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
            _fixture = new Fixture();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _controller = new CandidatesController(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateCandidate_ReturnsConflict_WhenCandidateExists()
        {
            // Arrange
            var candidateDto = _fixture.Build<CandidateDto>()
                .With(x => x.Email, string.Empty)
                .Create();
            var candidateModel = _fixture.Create<CandidateModel>();

            _unitOfWorkMock.Setup(u => u.CandidateRepository.GetCandidateByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(candidateModel);

            var jsonContent = new StringContent(JsonConvert.SerializeObject(candidateDto), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/candidates", jsonContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be("A candidate with the same email already exists.");
        }

        [Fact]
        public async Task CreateCandidate_ReturnsCreated_WhenCandidateDoesNotExist()
        {
            // Arrange
            var candidateDto = _fixture.Create<CandidateDto>();
            var candidateModel = _fixture.Build<CandidateModel>()
                                         .With(c => c.Email, candidateDto.Email)
                                         .Create();

            _unitOfWorkMock.Setup(u => u.CandidateRepository.GetCandidateByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((CandidateModel)null);

            _mapperMock.Setup(m => m.Map<CandidateModel>(It.IsAny<CandidateDto>()))
                .Returns(candidateModel);

            // Act
            var result = await _controller.CreateCandidate(candidateDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(CandidatesController.GetCandidateByEmail), createdAtActionResult.ActionName);
            Assert.Equal(candidateDto.Email, ((CandidateDto)createdAtActionResult.Value).Email);
        }
    }
}