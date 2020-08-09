using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TripleDerby.Core.Entities
{
    public class Horse
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public byte ColorId { get; set; }

        public virtual Color Color { get; set; } = default!;

        public byte LegTypeId { get; set; }
        
        public virtual LegType LegType { get; set; } = default!;

        public bool IsMale { get; set; }
        
        public Guid? SireId { get; set; }
        
        public Guid? DamId { get; set; }
        
        public short RaceStarts { get; set; }
        
        public short RaceWins { get; set; }
        
        public short RacePlace { get; set; }
        
        public short RaceShow { get; set; }
        
        public int Earnings { get; set; }
        
        public bool IsRetired { get; set; }
        
        public short Parented { get; set; }
        
        public Guid OwnerId { get; set; }

        public virtual ICollection<HorseStatistic> Statistics { get; set; } = new Collection<HorseStatistic>();
    }
}
