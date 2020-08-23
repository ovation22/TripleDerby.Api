using System;
using Ardalis.Specification;
using TripleDerby.Core.Entities;

namespace TripleDerby.Core.Specifications
{
    public sealed class HorseRandomRetiredSpecification : Specification<Horse>, ISpecification<Horse>
    {
        public HorseRandomRetiredSpecification(bool isMale)
        {
            Take = 10;
            Query
                .Where(x => x.IsMale == isMale && x.IsRetired)
                .OrderBy(x => Guid.NewGuid());
            Query.Include(x => x.Color);
        }

        public new int Take { get; internal set; }
    }
}