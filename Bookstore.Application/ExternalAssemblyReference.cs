using System.Reflection;

namespace Bookstore.Application
{
    public class ExternalAssemblyReference
    {
        public static readonly Assembly Assembly = typeof(ExternalAssemblyReference).Assembly;
    }
}
