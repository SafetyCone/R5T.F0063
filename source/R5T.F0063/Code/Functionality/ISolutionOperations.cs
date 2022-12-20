using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0132;


namespace R5T.F0063
{
	[FunctionalityMarker]
	public partial interface ISolutionOperations : IFunctionalityMarker
	{
		/// <inheritdoc cref="AddMissingDependencies(string)"/>
		public Task AddAllRecursiveProjectReferenceDependencies(string solutionFilePath)
		{
			return this.AddMissingDependencies(solutionFilePath);
		}

		/// <summary>
		/// Adds all missing recursive dependencies of projects within the solution.
		/// </summary>
		public async Task AddMissingDependencies(string solutionFilePath)
        {
			var missingDependencyProjectFilePaths = await this.GetMissingDependencies(solutionFilePath);

			Instances.SolutionFileOperator.AddProjects_InSolutionFolder(
				solutionFilePath,
				missingDependencyProjectFilePaths,
				Instances.SolutionFolderNames.Dependencies);
        }

		/// <summary>
		/// Computes the recursive project references missing from the solution that are referenced by projects in the solution.
		/// </summary>
		/// <returns>Project file paths that are recursive dependencies of projects in the solution that are missing from the solution.</returns>
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

		public async Task RemoveExtraneousDependencies(string solutionFilePath)
        {
			await Instances.SolutionFileOperator.InModifyContext(
				solutionFilePath,
				async (solutionFile, solutionFilePath) =>
				{
					// Get non-dependency project file paths.
					var nonDependencyProjectFilePaths = Instances.SolutionFileOperator.Get_NonDependencyProjectReferenceFilePaths(solutionFile, solutionFilePath);

					var allRecursiveProjectReferenceFilePaths = await Instances.ProjectReferencesOperator.GetAllRecursiveProjectReferences(
						nonDependencyProjectFilePaths);

					var allSolutionProjectFilePaths = Instances.SolutionFileOperator.Get_AllProjectReferenceFilePaths(solutionFile, solutionFilePath);

					var extraneousProjectDependencyFilePaths = allSolutionProjectFilePaths
						.Except(allRecursiveProjectReferenceFilePaths)
						// Non-dependency project file paths are never extraneous.
						.Except(nonDependencyProjectFilePaths)
						.Now();

                    foreach (var extraneousProjectDependencyFilePath in extraneousProjectDependencyFilePaths)
                    {
                        Instances.SolutionFileOperator.RemoveProject(
							solutionFile,
							solutionFilePath,
							extraneousProjectDependencyFilePath);
                    }
				});
		}
	}
}