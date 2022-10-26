using System;
using System.Collections.Generic;

using R5T.T0132;


namespace R5T.F0063.F001
{
	[FunctionalityMarker]
	public partial interface ISolutionOperator : IFunctionalityMarker,
		F0024.ISolutionOperator
	{
		public Dictionary<string, Guid> AddDependencyProjects_Idempotent(
			string solutionFilePath,
			IEnumerable<string> projectReferenceFilePaths)
		{
			var projectsMissingFromSolution = this.GetProjectsMissingFromSolution(
				solutionFilePath,
				projectReferenceFilePaths);

			var output = this.AddDependencyProjects(
				solutionFilePath,
				projectsMissingFromSolution);

			return output;
		}

		public Dictionary<string, Dictionary<string, Guid>> AddDependencyProjects_Idempotent(
			IEnumerable<string> solutionFilePaths,
			IEnumerable<string> projectReferenceFilePaths)
        {
			var output = new Dictionary<string, Dictionary<string, Guid>>();

            foreach (var solutionFilePath in solutionFilePaths)
            {
				var projectsAdded = this.AddDependencyProjects_Idempotent(
					solutionFilePath,
					projectReferenceFilePaths);

				output.Add(solutionFilePath, projectsAdded);
            }

			return output;
        }

		public Dictionary<string, Guid> AddDependencyProjects(
			string solutionFilePath,
			IEnumerable<string> projectFilePaths)
        {
			var output = F0024.SolutionFileOperator.Instance.AddProjects_InSolutionFolder(
				solutionFilePath,
				projectFilePaths,
				SolutionFolderNames.Instance.Dependencies);

			return output;
        }
	}
}