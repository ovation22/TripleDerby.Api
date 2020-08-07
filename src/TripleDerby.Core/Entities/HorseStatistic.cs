using System;
using System.ComponentModel.DataAnnotations;

namespace TripleDerby.Core.Entities
{
    public class HorseStatistic
    {
        [Key]
        public Guid Id { get; set; }

        public Guid HorseId { get; set; }

        public Guid StatisticId { get; set; }
        
        public Guid OwnerId { get; set; }

        public byte Actual { get; set; }

        public byte DominantPotential { get; set; }

        public byte RecessivePotential { get; set; }
    }
}
