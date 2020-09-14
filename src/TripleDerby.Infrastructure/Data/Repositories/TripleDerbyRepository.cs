using TripleDerby.Core.Interfaces.Repositories;

namespace TripleDerby.Infrastructure.Data.Repositories
{
    public class TripleDerbyRepository : EFRepository, ITripleDerbyRepository
    {
        public TripleDerbyRepository(TripleDerbyContext context) : base(context)
        {
        }
    }
}
