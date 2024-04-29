using System.Reflection;

namespace Bookstore.Infrastructure
{
    public class ExternalAssemblyReference
    {
        public static readonly Assembly Assembly = typeof(ExternalAssemblyReference).Assembly;
    }
}
