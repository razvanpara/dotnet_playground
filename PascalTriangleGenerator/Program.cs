using System;

namespace PascalTriangleGenerator
{
    class Program
    {
        static int[] GetStartingRow(int seed)
        {
            return new int[] { seed };
        }
        static int GetCellValue(int currentIndex, int[] previousRow)
        {
            if (currentIndex == 0)
                return previousRow[0];

            if (currentIndex == previousRow.Length)
                return previousRow[^1];

            return previousRow[currentIndex - 1] + previousRow[currentIndex];
        }
        static int[] GetNextRow(int[] previousRow)
        {
            //    1
            //   1 1
            //  1 2 1
            // 1 3 3 1
            var newRow = new int[previousRow.Length + 1];
            for (int i = 0; i < newRow.Length; i++)
                newRow[i] = GetCellValue(i, previousRow);
            return newRow;
        }
        static void Main(string[] args)
        {
            int[] row = null;
            var currentRow = 0;
            var rows = 6;
            Console.WriteLine(string.Join(" ", row));
            while (currentRow++ < rows)
            {
                row = GetNextRow(row);
                Console.WriteLine(string.Join(" ", row));
            }
        }
    }
}
