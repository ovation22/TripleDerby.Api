using System;
using System.Collections.Generic;

namespace TripleDerby.Core.DTOs
{
    public class HorsesResult
    {
        public PaginationInfo PaginationInfo { get; set; } = default!;
        
        public IEnumerable<Horse> Horses { get; set; } = new List<Horse>();

        public class Horse
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = default!;

            public string Color { get; set; } = default!;

            public short RaceStarts { get; set; }

            public short RaceWins { get; set; }

            public short RacePlace { get; set; }

            public short RaceShow { get; set; }

            public int Earnings { get; set; }
        }
    }
}
