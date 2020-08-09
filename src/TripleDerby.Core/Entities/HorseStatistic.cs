using System;
using System.ComponentModel.DataAnnotations;
using TripleDerby.Core.Enums;

namespace TripleDerby.Core.Entities
{
    public class HorseStatistic
    {
        [Key]
        public Guid Id { get; set; }

        public Guid HorseId { get; set; }

        public byte Actual { get; set; }

        public byte DominantPotential { get; set; }

        public byte RecessivePotential { get; set; }

        public virtual StatisticId StatisticId { get; set; } = default!;
    }
}
