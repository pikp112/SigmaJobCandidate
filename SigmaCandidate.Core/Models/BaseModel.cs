namespace SigmaCandidate.Core.Models
{
    public record BaseModel
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}