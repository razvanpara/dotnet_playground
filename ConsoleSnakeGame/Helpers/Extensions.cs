using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnakeGame.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Rotate in place a 2D array a number of rotations
        /// </summary>
        /// <typeparam name="T">The array's type</typeparam>
        /// <param name="arr">The array to be rotated</param>
        /// <param name="rotations">An integer representing the rotation type and number of rotations. 
        /// A positive number means clockwise rotation and a counter clockwise if negative</param>
        public static void RotateArr<T>(this T[,] arr, int rotations = 1)
        {

            Action<T[,], int, int, int, int> swap = (arr, r1, c1, r2, c2) =>
            {
                var temp = arr[r1, c1];
                arr[r1, c1] = arr[r2, c2];
                arr[r2, c2] = temp;
            };
            // a b c d      m i e a
            // e f g h  -\  n j f b
            // i j k l  -/  o k g c
            // m n o p      p l h d

            // 4/2 layers = 2
            // start = layer               // 0   // 1st layer
            // end = length - layer - 1    // 3

            // start = layer               // 1   // 2nd layer
            // end = length - layer - 1    // 2

            // corners
            // col_index    = row_index
            // top left     = c = start, r = start      (a)
            // top right    = c = end,   r = start      (d)
            // bot left     = c = start, r = end        (m)
            // bot right    = c = end,   r = end        (p)

            int side = arr.GetLength(0);
            int layers = side / 2;
            for (int layer = 0; layer < layers; layer++)
            {
                var start = layer;
                var end = side - 1 - layer;
                for (int r = 0; r < Math.Abs(rotations); r++)
                    for (int i = 0; i < end - start; i++)
                    {
                        var (tlr, tlc) = (start, start + i);
                        var (trr, trc) = (start + i, end);
                        var (blr, blc) = (end - i, start);
                        var (brr, brc) = (end, end - i);
                        swap(arr, tlr, tlc, trr, trc); // swap top left & top right
                        if (rotations > 0) // clockwise rotation
                        {
                            swap(arr, tlr, tlc, brr, brc); // swap top left & bot right
                            swap(arr, tlr, tlc, blr, blc); // swap top left & bot left
                        }
                        else // counter clockwise rotation
                        {
                            swap(arr, trr, trc, brr, brc); // swap top right & bot right
                            swap(arr, blr, blc, brr, brc); // swap bot left & bot right
                        }
                    }
            }
        }
        public static void Print2DArr<T>(this T[,] arr)
        {
            if (arr.Rank != 2 ||
                arr.GetLength(0) != arr.GetLength(1)
                )
                throw new ArgumentException("Array is not bidimensional or dimensions are not of equal length");

            var side = arr.GetLength(0);
            var sb = new StringBuilder();
            for (int r = 0; r < side; r++)
            {
                for (int c = 0; c < side; c++)
                    sb.Append($"{arr[r, c]} ");
                sb.Append(Environment.NewLine);
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
