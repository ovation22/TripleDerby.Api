using System;
using System.ComponentModel.DataAnnotations;

namespace TripleDerby.Core.Entities
{
    public class FeedingSession
    {
        [Key]
        public Guid Id { get; set; }
        
        public byte FeedingId { get; set; }

        public virtual Feeding Feeding { get; set; } = default!;

        public Guid HorseId { get; set; }

        public virtual Horse Horse { get; set; } = default!;

        public string Result { get; set; } = default!;
    }
}
