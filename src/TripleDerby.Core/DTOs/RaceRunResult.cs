using System;
using System.Collections.Generic;

namespace TripleDerby.Core.DTOs
{
    public class RaceRunResult
    {
        public Guid Id { get; set; }

        public byte RaceId { get; set; }

        public string WinHorse { get; set; } = default!;

        public string PlaceHorse { get; set; } = default!;

        public string ShowHorse { get; set; } = default!;

        public List<string> PlayByPlay { get; set; } = default!;
    }
}
