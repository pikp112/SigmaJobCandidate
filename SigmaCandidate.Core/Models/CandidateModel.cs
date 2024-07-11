using System.ComponentModel.DataAnnotations;

namespace SigmaCandidate.Core.Models
{
    public record CandidateModel : BaseModel
    {
        public required string FirstName { get; set; } = string.Empty;
        public required string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        [EmailAddress]
        public required string Email { get; set; } = string.Empty;
        public string? CallTimeInterval { get; set; } = string.Empty;
        [Url]
        public string? LinkedInProfileUrl { get; set; } = string.Empty;
        [Url]
        public string? GitHubProfileUrl { get; set; } = string.Empty;
        public required string Comment { get; set; } = string.Empty;
    }
}