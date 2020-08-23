using System;
using Ardalis.Specification;
using TripleDerby.Core.Entities;

namespace TripleDerby.Core.Specifications
{
    public class HorseRandomRacerSpecification : Specification<Horse>
    {
        public HorseRandomRacerSpecification()
        {
            Take = 10;
            Query
                .Where(x => !x.IsRetired)
                .OrderBy(x => Guid.NewGuid());
            Query.Include(x => x.Statistics);
        }

        public new int Take { get; internal set; }
    }
}
