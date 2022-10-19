using System;

using R5T.F0016;
using R5T.F0020;
using R5T.F0024;


namespace R5T.F0063
{
    public static class Instances
    {
        public static IProjectFileOperator ProjectFileOperator { get; } = F0020.ProjectFileOperator.Instance;
        public static IProjectReferencesOperator ProjectReferencesOperator { get; } = F0016.ProjectReferencesOperator.Instance;
        public static ISolutionFileOperator SolutionFileOperator { get; } = F0024.SolutionFileOperator.Instance;
        public static ISolutionFolderNames SolutionFolderNames { get; } = F0063.SolutionFolderNames.Instance;
    }
}