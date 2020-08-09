using System;
using TripleDerby.Core.Interfaces.Utilities;

namespace TripleDerby.Infrastructure.Utilities
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random _random;

        public RandomGenerator()
        {
            _random = new Random(DateTime.Now.Millisecond);
        }

        public int Next()
        {
            return _random.Next();
        }

        public int Next(int max)
        {
            return _random.Next(max);
        }

        public int Next(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
