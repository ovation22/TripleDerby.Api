﻿using System;

namespace TripleDerby.Core.DTOs
{
    public record ParentHorse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = default!;

        public string Color { get; init; } = default!;

        public short RaceStarts { get; init; }

        public short RaceWins { get; init; }

        public short RacePlace { get; init; }

        public short RaceShow { get; init; }

        public int Earnings { get; init; }
    }
}
