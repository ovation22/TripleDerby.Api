using Ardalis.Specification;
using TripleDerby.Core.Entities;

namespace TripleDerby.Core.Specifications
{
    public sealed class HorsesPaginatedSpecification : Specification<Horse>
    {
        public HorsesPaginatedSpecification(int skip, int take)
        {
            Query.Paginate(skip, take);
        }
    }
}
