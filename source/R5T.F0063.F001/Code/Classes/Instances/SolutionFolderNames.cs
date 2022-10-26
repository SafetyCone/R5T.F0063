using System;


namespace R5T.F0063.F001
{
	public class SolutionFolderNames : ISolutionFolderNames
	{
		#region Infrastructure

	    public static ISolutionFolderNames Instance { get; } = new SolutionFolderNames();

	    private SolutionFolderNames()
	    {
        }

	    #endregion
	}
}