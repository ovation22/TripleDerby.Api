using System;

namespace TripleDerby.Core.DTOs
{
    public class Foal
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public byte ColorId { get; set; }

        public string Color { get; set; } = default!;
    }
}
