﻿using System;

namespace TripleDerby.Core.DTOs
{
    public record Foal
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = default!;

        public byte ColorId { get; init; }

        public string Color { get; init; } = default!;
    }
}
