using Ardalis.Specification;
using TripleDerby.Core.Entities;

namespace TripleDerby.Core.Specifications
{
    public sealed class HorsesPaginatedSpecification : Specification<Horse>
    {
        public HorsesPaginatedSpecification(int skip, int take)
        {
            Query
                .Include(x => x.Color);

            Query
                .Skip(skip)
                .Take(take);
        }
    }
}
