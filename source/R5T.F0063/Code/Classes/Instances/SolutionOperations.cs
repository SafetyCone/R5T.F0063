using System;


namespace R5T.F0063
{
	public class SolutionOperations : ISolutionOperations
	{
		#region Infrastructure

	    public static ISolutionOperations Instance { get; } = new SolutionOperations();

	    private SolutionOperations()
	    {
        }

	    #endregion
	}
}