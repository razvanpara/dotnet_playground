using System;
using System.Linq;
using System.Numerics;

namespace CellularAutomationRuleGeneralized
{
    public class BigIntConversion
    {
        public static string ToBase2(BigInteger bigInt)
        {
            var bytes = bigInt.ToByteArray().ToList();
            //removing the leading extra bytes with value 0
            while (bytes.Count > 0 && bytes[^1] == 0)
                bytes.RemoveAt(bytes.Count - 1);

            //converting the byte array to the binary representation
            //each byte is forced to be 8 bits long by adding leading zeroes
            var stringRepresentation = string.Join("", bytes.ToArray().Reverse().Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));

            //getting the missing bits that keeps our binary representation from having bits count equal to a power of two
            var totalBits = (int)Math.Pow(2, Math.Ceiling(Math.Log2(stringRepresentation.Length)));

            //returning the string value with the missing bits as left padding
            return Convert.ToString(stringRepresentation).PadLeft(totalBits, '0');
        }
        public static BigInteger Base2ToBigInteger(string binaryRepr) => new BigInteger(binaryRepr.ToCharArray()
            .Select((c, i) => new { c, i })
            .GroupBy(o => o.i / 8)
            .Select(g => string.Join("", g.ToArray().Select(g => g.c)))
            .Select(b => Convert.ToByte(b, 2))
            .Reverse()
            .ToArray());
    }
}
