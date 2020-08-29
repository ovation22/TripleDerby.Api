using System;
using Ardalis.Specification;
using TripleDerby.Core.Entities;

namespace TripleDerby.Core.Specifications
{
    public class HorseRandomRacerSpecification : Specification<Horse>
    {
        public HorseRandomRacerSpecification()
        {
            Query
                .Where(x => !x.IsRetired)
                .OrderBy(x => Guid.NewGuid());
            Query.Include(x => x.Statistics);
            Query.Paginate(0, 10);
        }
    }
}
