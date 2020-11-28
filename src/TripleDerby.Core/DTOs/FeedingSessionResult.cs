using TripleDerby.Core.Enums;

namespace TripleDerby.Core.DTOs
{
    public record FeedingSessionResult
    {
        public FeedResponse Result { get; init; }
    }
}
