﻿namespace TripleDerby.Core.DTOs
{
    public record FeedingResult
    {
        public byte Id { get; init; }

        public string Name { get; init; } = default!;

        public string Description { get; init; } = default!;
    }
}
