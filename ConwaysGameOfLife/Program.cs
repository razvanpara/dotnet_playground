using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace ConwaysGameOfLife
{
    class Program
    {
        public static bool[][] GetBlankBoard(int rows, int cols)
        {
            bool[][] board = new bool[rows][];
            for (var row = 0; row < rows; row++)
                board[row] = new bool[cols];
            return board;
        }
        public static void DrawBoard(bool[][] board, Tuple<string, string> chars)
        {
            Console.Clear();
            int rows = board.Length;
            int cols = rows == 0 ? 0 : board[0].Length;
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                    sb.Append(board[row][col] ? chars.Item1 : chars.Item2);
                sb.Append('\n');
            }
            Console.WriteLine(sb.ToString());
        }
        public static bool GetCellValue(int row, int col, bool[][] board)
        {
            try
            {
                return board[row][col];
            }
            catch (Exception E)
            {
                return false;
            }
        }
        public static bool[] GetNeighbourCells(int cellRow, int cellCol, bool[][] board)
        {
            bool N = GetCellValue(cellRow - 1, cellCol, board),
                NE = GetCellValue(cellRow - 1, cellCol + 1, board),
                E = GetCellValue(cellRow, cellCol + 1, board),
                SE = GetCellValue(cellRow + 1, cellCol + 1, board),
                S = GetCellValue(cellRow + 1, cellCol, board),
                SW = GetCellValue(cellRow + 1, cellCol - 1, board),
                W = GetCellValue(cellRow, cellCol - 1, board),
                NW = GetCellValue(cellRow - 1, cellCol - 1, board);
            return new bool[] { N, NE, E, SE, S, SW, W, NW };
        }

        public static int GetAliveNeighbours(bool[] neighbours)
        {
            int alive = 0;
            for (var i = 0; i < 8; i++)
            {
                if (neighbours[i]) alive++;
            }
            return alive;
        }

        public static bool GetNextCellValue(int row, int col, bool[][] board)
        {
            var currentCell = GetCellValue(row, col, board);
            var neighbours = GetNeighbourCells(row, col, board);
            var aliveNeighbours = GetAliveNeighbours(neighbours);

            if (currentCell)
            {
                if (aliveNeighbours == 2 || aliveNeighbours == 3)
                    return true;
            }
            else
            {
                if (aliveNeighbours == 3)
                    return true;
            }
            return false;
        }

        public static bool[][] GetNextBoard(bool[][] board)
        {
            int rows = board.Length;
            int cols = rows == 0 ? 0 : board[0].Length;
            var nextBoard = GetBlankBoard(rows, cols);

            for (var row = 0; row < rows; row++)
                for (var col = 0; col < cols; col++)
                    nextBoard[row][col] = GetNextCellValue(row, col, board);
            return nextBoard;
        }
        public static void Main(string[] args)
        {
            //bool[][] board = new bool[][]
            //{
            //    new bool []{ true, false, false, false, true },
            //    new bool []{ false, true, false, true, false },
            //    new bool []{ false, false, true, false, false },
            //    new bool []{ false, true, false, true, false },
            //    new bool []{ true, false, false, false, true },
            //};
            var board = GetBlankBoard(50, 50);
            //board[3][5] = true;
            //board[4][6] = true;
            //board[5][6] = true;
            //board[5][5] = true;
            //board[5][4] = true;

            board[23][18] = true;
            board[24][16] = true;
            board[24][18] = true;
            board[25][17] = true;
            board[25][18] = true;
            board[26][27] = true;
            board[27][25] = true;
            board[27][26] = true;
            board[28][26] = true;
            board[28][27] = true;
            while (true)
            {
                DateTimeOffset before = DateTimeOffset.Now;
                DrawBoard(board, new Tuple<string, string>("X", " "));
                Console.WriteLine($"Drawing Time:\t\t{DateTimeOffset.Now.Subtract(before).TotalSeconds}");
                before = DateTimeOffset.Now;
                board = GetNextBoard(board);
                Console.WriteLine($"Board Solving Time:\t{DateTimeOffset.Now.Subtract(before).TotalSeconds}");
                Thread.Sleep(33);
            }
        }
    }
}