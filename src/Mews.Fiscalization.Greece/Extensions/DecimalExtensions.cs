using System;

namespace Mews.Fiscalization.Greece.Extensions
{
    internal static class DecimalExtensions
    {
        internal static int GetDecimalPlaces(this decimal value)
        {
            return BitConverter.GetBytes(Decimal.GetBits(value)[3])[2];
        }
    }
}
