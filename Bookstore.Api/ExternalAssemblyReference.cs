using System.Reflection;

namespace Bookstore.Api
{
    public class ExternalAssemblyReference
    {
        public static readonly Assembly Assembly = typeof(ExternalAssemblyReference).Assembly;
    }
}
