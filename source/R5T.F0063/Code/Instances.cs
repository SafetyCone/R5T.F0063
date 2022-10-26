using System;

using R5T.F0020;
using R5T.F0024;


namespace R5T.F0063
{
    public static class Instances
    {
        public static IGlobalSectionOperator GlobalSectionOperator { get; } = F0024.GlobalSectionOperator.Instance;
        public static INestedProjectsGlobalSectionOperator NestedProjectsGlobalSectionOperator { get; } = F0024.NestedProjectsGlobalSectionOperator.Instance;
        public static IProjectFileOperator ProjectFileOperator { get; } = F0020.ProjectFileOperator.Instance;
        public static F0016.F001.IProjectReferencesOperator ProjectReferencesOperator { get; } = F0016.F001.ProjectReferencesOperator.Instance;
        public static ISolutionFileOperator SolutionFileOperator { get; } = F0063.SolutionFileOperator.Instance;
        public static F001.ISolutionFolderNames SolutionFolderNames { get; } = F001.SolutionFolderNames.Instance;
    }
}