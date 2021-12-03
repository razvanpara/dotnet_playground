using System;
using System.Collections.Generic;
using System.Linq;

namespace CellularAutomationRule
{
    class Program
    {
        static bool GetNextCellValue(int index, int rule, bool[] board)
        {
            var indexes = GetIndexes(index, board.Length);
            var ruleNo = GetRuleNumber(indexes, board);
            return ((rule >> ruleNo) & 1) == 1;
        }
        static int[] GetIndexes(int index, int boardSize)
        {
            int prevIndex = index - 1;
            int nextIndex = (index + 1) % boardSize;
            if (index == 0)
                prevIndex = boardSize - 1;
            return new int[] { prevIndex, index, nextIndex };
        }
        static short GetRuleNumber(int[] indexes, bool[] board)
        {
            short ruleNo = 0;
            for (short i = 0; i < indexes.Length; i++)
            {
                var boardValue = board[indexes[i]];
                if (boardValue)
                {
                    int powerOfTwo = indexes.Length - 1 - i;
                    short result = 1;
                    while (powerOfTwo-- > 0)
                    {
                        result *= 2;
                    }
                    ruleNo += result;
                }
            }
            return ruleNo;
        }
        static bool[] GetStartingBoard(int boardSize, bool randomStart = false)
        {
            var random = new Random();
            var board = new bool[boardSize];
            board[boardSize / 2] = true;
            if (randomStart)
                board = board.Select(b => random.Next(0, 2) == 1).ToArray();
            return board;
        }
        static bool[] SolveBoard(int rule, bool[] board)
        {
            var length = board.Length;
            var newBoard = new bool[length];

            for (int i = 0; i < newBoard.Length; i++)
                newBoard[i] = GetNextCellValue(i, rule, board);

            return newBoard;
        }
        static void PrintBoard(bool[] board) => Console.WriteLine(string.Join("", board.Select(val => val ? "#" : " ")));
        static void Main(string[] args)
        {
            byte rule = 110;
            int boardSize = Console.WindowWidth;
            int iterations = 1_000_000;
            int fps = 24;
            bool randomStart = false;

            var startingBoard = GetStartingBoard(boardSize, randomStart);
            while (iterations-- > 0)
            {
                PrintBoard(startingBoard);
                startingBoard = SolveBoard(rule, startingBoard);
                System.Threading.Thread.Sleep(1000 / fps);
            }
        }
    }
}
