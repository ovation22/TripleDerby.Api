using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using TripleDerby.Core.Enums;

namespace TripleDerby.Core.Entities
{
    public class Horse
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public bool IsMale { get; set; }
        
        public Guid SireId { get; set; }
        
        public Guid DamId { get; set; }
        
        public short RaceStarts { get; set; }
        
        public short RaceWins { get; set; }
        
        public short RacePlaces { get; set; }
        
        public short RaceShows { get; set; }
        
        public int Earnings { get; set; }
        
        public bool IsRetired { get; set; }
        
        public int Parented { get; set; }
        
        public Guid OwnerId { get; set; }

        public virtual Collection<HorseStatistic> Statistics { get; set; } = new Collection<HorseStatistic>();

        public virtual Color Color { get; set; } = default!;
        public virtual LegType LegType { get; set; } = default!;
    }
}
