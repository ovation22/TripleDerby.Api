using System;
using TripleDerby.Core.Interfaces.Utilities;

namespace TripleDerby.Infrastructure.Utilities
{
    public class TimeManager : ITimeManager
    {
        public DateTime Now() => DateTime.Now;

        public DateTime UtcNow() => DateTime.UtcNow;
    }
}