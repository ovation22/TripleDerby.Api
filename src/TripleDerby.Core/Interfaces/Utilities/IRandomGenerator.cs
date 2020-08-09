﻿namespace TripleDerby.Core.Interfaces.Utilities
{
    public interface IRandomGenerator
    {
        int Next();
        int Next(int max);
        int Next(int min, int max);
    }
}
