namespace SigmaCandidate.Core.Models
{
    public record CandidateModel : BaseModel
    {
        public required string FirstName { get; init; } = string.Empty;
        public required string LastName { get; init; } = string.Empty;
        public string? PhoneNumber { get; init; } = string.Empty;
        public required string Email { get; init; } = string.Empty;
        public string? CallTimeInterval { get; init; } = string.Empty;
        public string? LinkedInProfileUrl { get; init; } = string.Empty;
        public string? GitHubProfileUrl { get; init; } = string.Empty;
        public required string Comment { get; init; } = string.Empty;
    }
}