using System;
using System.Threading.Tasks;

using R5T.T0132;


namespace R5T.F0063.Construction
{
	[FunctionalityMarker]
	public partial interface ISolutionOperations : IFunctionalityMarker,
		F0063.ISolutionOperations
	{
		public async Task RemoveExtraneousDependencies()
        {
			/// Inputs.
			var solutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0046\source\R5T.S0046.sln";

			/// Run.
			await this.RemoveExtraneousDependencies(solutionFilePath);
		}
	}
}