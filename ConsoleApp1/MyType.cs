using ExtendedServiceProvider;
using System.Diagnostics;

namespace ConsoleApp1
{
    [DebuggerDisplay("Name={Name}")]
    internal class MyType : IMyType
    {
        [Inject]
        public string? Name { get; set; }
    }
}