using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Extensions
{
    internal static class IdentifierExtensions
    {
        internal static bool IsDefined<T>(this Identifier<T> value)
        {
            return value != null;
        }

        internal static T GetOrDefault<T>(this Identifier<T> value)
        {
            if (!value.IsDefined())
            {
                return default(T);
            }

            return value.Value;
        }
    }
}
