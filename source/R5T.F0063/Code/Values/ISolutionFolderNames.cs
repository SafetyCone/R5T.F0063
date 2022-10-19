using System;

using R5T.T0131;


namespace R5T.F0063
{
	[ValuesMarker]
	public partial interface ISolutionFolderNames : IValuesMarker
	{
		public string Dependencies => "_Dependencies";
	}
}