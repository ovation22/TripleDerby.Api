using System.ComponentModel.DataAnnotations;
using TripleDerby.Core.Enums;

namespace TripleDerby.Core.Entities
{
    public class Track
    {
        [Key]
        public TrackId Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
