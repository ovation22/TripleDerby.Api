using Ardalis.Specification;
using TripleDerby.Core.Entities;

namespace TripleDerby.Core.Specifications
{
    public sealed class RaceSpecification : Specification<Race>
    {
        public RaceSpecification(byte id)
        {
            Query.Where(x => x.Id == id);

            Query.Include(x => x.Track);

            Query.Include(x => x.Surface);
        }
    }
}
