using System;


namespace R5T.F0063.Construction
{
    public static class Instances
    {
        public static ISolutionOperations SolutionOperations { get; } = Construction.SolutionOperations.Instance;
    }
}