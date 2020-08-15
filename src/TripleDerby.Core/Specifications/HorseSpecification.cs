using System;
using Ardalis.Specification;
using TripleDerby.Core.Entities;

namespace TripleDerby.Core.Specifications
{
    public sealed class HorseSpecification : Specification<Horse>
    {
        public HorseSpecification(Guid id)
        {
            Query.Where(x => x.Id == id);
        }
    }
}
