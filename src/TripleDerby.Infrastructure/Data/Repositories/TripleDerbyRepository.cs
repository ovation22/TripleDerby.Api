using TripleDerby.Core.Interfaces.Repositories;

namespace TripleDerby.Infrastructure.Data.Repositories
{
    public class TripleDerbyRepository : EFRepository, ITripleDerbyRepository
    {
        private readonly TripleDerbyContext _context;

        public TripleDerbyRepository(TripleDerbyContext context) : base(context)
        {
            _context = context;
        }
    }
}
