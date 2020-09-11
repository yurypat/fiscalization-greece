using System;

namespace Mews.Fiscalization.Greece.Extensions
{
    internal static class EnumExtensions
    {
        internal static T ConvertToEnum<T>(this Enum source) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), source.ToString(), true);
        }
    }
}
