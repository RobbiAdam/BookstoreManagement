using System.Reflection;

namespace Bookstore.Domain
{
    public class ExternalAssemblyReference
    {
        public static readonly Assembly Assembly = typeof(ExternalAssemblyReference).Assembly;
    }
}
