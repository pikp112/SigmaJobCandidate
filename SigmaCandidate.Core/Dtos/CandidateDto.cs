using System.ComponentModel.DataAnnotations;

namespace SigmaCandidate.Core.Dtos
{
    public record CandidateDto
    {
        [Required]
        public string FirstName { get; init; } = string.Empty;
        [Required]
        public string LastName { get; init; } = string.Empty;
        [Phone]
        public string? PhoneNumber { get; init; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; init; } = string.Empty;
        public string? CallTimeInterval { get; init; } = string.Empty;
        [Url]
        public string? LinkedInProfileUrl { get; init; } = string.Empty;
        [Url]
        public string? GitHubProfileUrl { get; init; } = string.Empty;
        [Required]
        public string Comment { get; init; } = string.Empty;
    }
}