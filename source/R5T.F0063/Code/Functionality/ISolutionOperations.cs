using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0132;


namespace R5T.F0063
{
	[FunctionalityMarker]
	public partial interface ISolutionOperations : IFunctionalityMarker
	{
		public async Task AddMissingDependencies(string solutionFilePath)
        {
			var missingDependencyProjectFilePaths = await this.GetMissingDependencies(solutionFilePath);

			Instances.SolutionFileOperator.AddProjects_InSolutionFolder(
				solutionFilePath,
				missingDependencyProjectFilePaths,
				Instances.SolutionFolderNames.Dependencies);
        }

		/// <summary>
		/// Computes the project references missing from the solution that are referenced by projects in the solution.
		/// </summary>
		/// <returns>Project file paths that are dependencies of projects in the solution that are missing from the solution.</returns>
		public async Task<string[]> GetMissingDependencies(string solutionFilePath)
        {
			var projectReferenceFilePaths = Instances.SolutionFileOperator.Get_ProjectReferenceFilePaths(solutionFilePath);

			var allRecursiveProjectReferenceFilePaths = await this.GetAllRecursiveProjectReferences(solutionFilePath);

			var missingDependencies = allRecursiveProjectReferenceFilePaths
				.Except(projectReferenceFilePaths)
				.ToArray();

			return missingDependencies;
        }

		public async Task<string[]> GetAllRecursiveProjectReferences(string solutionFilePath)
        {
			var projectReferenceFilePaths = Instances.SolutionFileOperator.Get_ProjectReferenceFilePaths(solutionFilePath);

			var allRecursiveProjectReferences = await Instances.ProjectReferencesOperator.GetAllRecursiveProjectReferences(
				projectReferenceFilePaths,
				Instances.ProjectFileOperator.GetDirectProjectReferenceFilePaths);

			return allRecursiveProjectReferences;
		}
	}
}