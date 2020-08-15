using System;
using Ardalis.Specification;
using TripleDerby.Core.Entities;

namespace TripleDerby.Core.Specifications
{
    public sealed class RandomRetiredHorseSpecification : Specification<Horse>, ISpecification<Horse>
    {
        public RandomRetiredHorseSpecification(bool isMale)
        {
            Take = 10;
            Query
                .Where(x => x.IsMale == isMale && x.IsRetired)
                .OrderBy(x => Guid.NewGuid());
        }

        public new int Take { get; internal set; }
    }
}