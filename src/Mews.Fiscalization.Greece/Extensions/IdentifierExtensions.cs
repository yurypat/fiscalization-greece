using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Extensions
{
    internal static class IdentifierExtensions
    {
        internal static bool IsNotNull<T>(this Identifier<T> value)
        {
            return value != null;
        }
    }
}
