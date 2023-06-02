using System;

using R5T.T0131;


namespace R5T.F0063
{
    [ValuesMarker]
    public partial interface IValues : IValuesMarker
    {
        /// <summary>
        /// true, the default value ensures that recursive project references are added when adding a project to a solution.
        /// </summary>
        public const bool Default_AddRecursiveProjectReferences_Constant = true;

        /// <inheritdoc cref="Default_AddRecursiveProjectReferences_Constant"/>
        public bool Default_AddRecursiveProjectReferences => IValues.Default_AddRecursiveProjectReferences_Constant;
    }
}
