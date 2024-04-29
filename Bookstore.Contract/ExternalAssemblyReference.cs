using System.Reflection;

namespace Bookstore.Contract
{
    public class ExternalAssemblyReference
    {
        public static readonly Assembly Assembly = typeof(ExternalAssemblyReference).Assembly;
    }
}
