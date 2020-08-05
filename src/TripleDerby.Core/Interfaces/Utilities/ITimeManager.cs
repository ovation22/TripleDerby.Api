using System;

namespace TripleDerby.Core.Interfaces.Utilities
{
    public interface ITimeManager
    {
        DateTime Now();
        DateTime UtcNow();
    }
}
