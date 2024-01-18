using System;
using System.Linq;

using R5T.F0024.T001;
using R5T.T0132;
using R5T.T0221;


namespace R5T.F0063
{
    [FunctionalityMarker]
	public partial interface ISolutionFileOperator : IFunctionalityMarker,
		F0024.ISolutionFileOperator
	{
		/// <summary>
		/// Gets all project references.
		/// (Which is all project references in the solution minus those that are solution folders, which is necessary because solution folders are represented in the solution file as project references.)
		/// </summary>
		public ProjectFileReference[] Get_AllProjectReferences(SolutionFile solutionFile)
        {
			var allProjectReferences = this.Get_ProjectFileReferences(solutionFile);
			return allProjectReferences;
        }

		/// <summary>
		/// Gets all project reference file paths.
		/// (Which is all project references in the solution minus those that are solution folders, which is necessary because solution folders are represented in the solution file as project references.)
		/// </summary>
		public string[] Get_AllProjectReferenceFilePaths(SolutionFile solutionFile, string solutionFilePath)
		{
			var allProjectReferenceFilePaths = this.Get_ProjectReferenceFilePaths(solutionFile, solutionFilePath);
			return allProjectReferenceFilePaths;
		}

		public ProjectFileReference[] Get_DependencyProjectReferences(SolutionFile solutionFile)
        {
			var hasDependenciesSolutionFolder = this.Has_DependenciesSolutionFolderProjectReference(solutionFile);
			if (!hasDependenciesSolutionFolder)
			{
				// If there is no dependencies solution folder, then all project references are non-dependency project references.
				return Array.Empty<ProjectFileReference>();
			}

			var nestedProjectsGlobalSection = this.Get_NestedProjectsGlobalSection(solutionFile);

			var dependenciesProjectFileReferences = Instances.NestedProjectsGlobalSectionOperator.GetProjectFileReferencesInParent(
				nestedProjectsGlobalSection,
				solutionFile.ProjectFileReferences,
				hasDependenciesSolutionFolder.Result.ProjectIdentity);

			return dependenciesProjectFileReferences;
		}

		public ProjectFileReference[] Get_NonDependencyProjectReferences(SolutionFile solutionFile)
        {
			var allProjectReferences = this.Get_ProjectFileReferences(solutionFile);

			var dependencyProjectReferences = this.Get_DependencyProjectReferences(solutionFile);

			var nonDependencyProjectReferences = allProjectReferences
				.Except(
					dependencyProjectReferences,
					ProjectFileReferenceIdentityBasedEqualityComparer.Instance)
				.Now();

			return nonDependencyProjectReferences;
        }

		public string[] Get_NonDependencyProjectReferenceFilePaths(SolutionFile solutionFile, string solutionFilePath)
		{
			var nonDependencyProjectReferences = this.Get_NonDependencyProjectReferences(solutionFile);

			var nonDependencyProjectReferenceFilePaths = this.Get_ProjectReferenceFilePaths(
				nonDependencyProjectReferences,
				solutionFilePath);

			return nonDependencyProjectReferenceFilePaths;
		}

		public WasFound<ProjectFileReference> Has_DependenciesSolutionFolderProjectReference(SolutionFile solutionFile)
        {
			var dependenciesOrDefault = solutionFile.ProjectFileReferences
				.WhereIsSolutionFolder()
				.SingleOrDefault();

			var wasFound = WasFound.From(dependenciesOrDefault);
			return wasFound;
        }
	}
}