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

        private static void Swap<T>(this T[,] arr, int aX, int aY, int bX, int bY)
        {
            T temp = arr[aX, aY];
            arr[aX, aY] = arr[bX, bY];
            arr[bX, bY] = temp;
        }
        private static void MoveEdge<T>(this T[,] arr, int holderRow, int holderCol, int rowStart, int colStart,
            Func<int, int> rowModifier, Func<int, int> colModifier, Func<int, int, bool> predicate)
        {
            // recursive
            if (predicate(rowStart, colStart))
            {
                arr.Swap(holderRow, holderCol, rowStart, colStart);
                arr.MoveEdge(holderRow, holderCol, rowModifier(rowStart), colModifier(colStart), rowModifier, colModifier, predicate);
            }

            // iterative
            //for (int row = rowStart,
            //col = colStart;
            //predicate(row, col);
            //row = rowModifier(row),
            //col = colModifier(col))
            //    arr.Swap(holderRow, holderCol, row, col);
        }

        private static void RotateLayer<T>(this T[,] arr, int layer, int elements)
        {
            int layerStart = layer;
            int layerLastIndex = arr.GetLength(0) - 1 - layer;
            for (int i = 0; i < Math.Abs(elements); i++)
                if (elements > 0) // clockwise rotation
                {
                    // top edge
                    arr.MoveEdge(layerStart, layerStart, layerStart, layerStart + 1, row => row + 0, col => col + 1, (row, col) => col <= layerLastIndex);
                    // right edge
                    arr.MoveEdge(layerStart, layerStart, layerStart + 1, layerLastIndex, row => row + 1, col => col + 0, (row, col) => row <= layerLastIndex);
                    // bottom edge
                    arr.MoveEdge(layerStart, layerStart, layerLastIndex, layerLastIndex - 1, row => row + 0, col => col - 1, (row, col) => col >= layerStart);
                    // left edge
                    arr.MoveEdge(layerStart, layerStart, layerLastIndex - 1, layerStart, row => row - 1, col => col + 0, (row, col) => row >= layerStart);
                }
                else // counter clockwise rotation
                {
                    // top edge
                    arr.MoveEdge(layerStart, layerLastIndex, layerStart, layerLastIndex - 1, row => row + 0, col => col - 1, (row, col) => col >= layerStart);
                    // left edge
                    arr.MoveEdge(layerStart, layerLastIndex, layerStart + 1, layerStart, row => row + 1, col => col + 0, (row, col) => row <= layerLastIndex);
                    // bottom edge
                    arr.MoveEdge(layerStart, layerLastIndex, layerLastIndex, layerStart + 1, row => row + 0, col => col + 1, (row, col) => col <= layerLastIndex);
                    // right edge
                    arr.MoveEdge(layerStart, layerLastIndex, layerLastIndex - 1, layerLastIndex, row => row - 1, col => col + 0, (row, col) => row >= layerStart);
                }
        }
        public static void RotateByElement<T>(this T[,] arr, int elements = 1)
        {
            var length = arr.GetLength(0);
            var layers = length / 2;
            for (int layer = 0; layer < layers; layer++)
                arr.RotateLayer(layer, elements);
        }
        public static void RotateByDegrees<T>(this T[,] arr, int degrees = 1)
        {
            // . . .    . . . .     . . . . .
            // . . .    . . . .     . . . . .
            // . . .    . . . .     . . . . .
            //          . . . .     . . . . .
            //                      . . . . .
            var sideElements = arr.GetLength(0);


            var length = arr.GetLength(0);
            var layers = length / 2;
            for (int layer = 0; layer < layers; layer++)
            {
                var outerElementsCount = (sideElements - layer * 2) * 4 - 4;
                var degreesPerElement = (double)360 / outerElementsCount;
                var elementsToRotate = (int)Math.Floor(degrees / degreesPerElement);
                arr.RotateLayer(layer, elementsToRotate);
            }
        }
    }
}
