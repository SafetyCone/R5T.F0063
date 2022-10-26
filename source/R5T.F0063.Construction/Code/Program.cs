using System;
using System.Threading.Tasks;


namespace R5T.F0063.Construction
{
    class Program
    {
        static async Task Main()
        {
            await Instances.SolutionOperations.RemoveExtraneousDependencies();
        }
    }
}