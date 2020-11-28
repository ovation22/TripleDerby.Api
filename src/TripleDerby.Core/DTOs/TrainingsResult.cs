namespace TripleDerby.Core.DTOs
{
    public record TrainingsResult
    {
        public byte Id { get; init; }

        public string Name { get; init; } = default!;

        public string Description { get; init; } = default!;
    }
}
