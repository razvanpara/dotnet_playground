using System;
using System.Linq;
using System.Numerics;

namespace CellularAutomationRuleGeneralized
{
    public class BigIntConversion
    {
        public static string ToBase2(BigInteger bigInt) => string.Join("", bigInt.ToByteArray().Reverse().Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
        public static BigInteger Base2ToBigInteger(string binaryRepr) => new BigInteger(binaryRepr.ToCharArray()
            .Select((c, i) => new { c, i })
            .GroupBy(o => o.i / 8)
            .Select(g => string.Join("", g.ToArray().Select(g => g.c)))
            .Select(b => Convert.ToByte(b, 2))
            .Reverse()
            .ToArray());
    }
}
