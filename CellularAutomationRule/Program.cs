using System;
using System.Collections.Generic;
using System.Linq;

namespace CellularAutomationRule
{
    class Program
    {
        static (int, int, int) GetIndexes(int index, int boardSize)
        {
            if (index == 0)
            {
                return (boardSize - 1, index, index + 1);
            }
            if (index == boardSize - 1)
            {
                return (index - 1, index, 0);
            }
            return (index - 1, index, index + 1);
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
        static bool[] SolveBoard(bool[] board, Dictionary<(bool, bool, bool), bool> ruleMapping)
        {
            var length = board.Length;
            var newBoard = new bool[length];

            for (int i = 0; i < newBoard.Length; i++)
                newBoard[i] = GetValue(i, board, ruleMapping);

            return newBoard;
        }
        static bool GetValue(int index, bool[] sourceBoard, Dictionary<(bool, bool, bool), bool> ruleMapping)
        {
            var indexes = GetIndexes(index, sourceBoard.Length);
            var v1 = sourceBoard[indexes.Item1];
            var v2 = sourceBoard[indexes.Item2];
            var v3 = sourceBoard[indexes.Item3];
            //return (v1, v2, v3) switch
            //{
            //    (true, true, true) => false,    //7
            //    (true, true, false) => true,    //6
            //    (true, false, true) => true,    //5
            //    (true, false, false) => false,  //4
            //    (false, true, true) => true,    //3
            //    (false, true, false) => true,   //2
            //    (false, false, true) => true,   //1
            //    (false, false, false) => false, //0
            //};
            return ruleMapping[(v1, v2, v3)];
        }
        static Dictionary<(bool, bool, bool), bool> GetRuleMapping(byte rule)
        {
            var ruleBinDigits = Convert.ToString((int)rule, 2).ToCharArray().Select(c => int.Parse($"{c}"));
            if (ruleBinDigits.Count() < 8) Enumerable.Repeat(0, 8 - ruleBinDigits.Count()).ToList().ForEach(d => ruleBinDigits = ruleBinDigits.Prepend(d));
            var binDigits = ruleBinDigits.ToArray();

            var ruleMapping = new Dictionary<(bool, bool, bool), bool>();

            ruleMapping.Add((true, true, true), binDigits[0] == 1);
            ruleMapping.Add((true, true, false), binDigits[1] == 1);
            ruleMapping.Add((true, false, true), binDigits[2] == 1);
            ruleMapping.Add((true, false, false), binDigits[3] == 1);
            ruleMapping.Add((false, true, true), binDigits[4] == 1);
            ruleMapping.Add((false, true, false), binDigits[5] == 1);
            ruleMapping.Add((false, false, true), binDigits[6] == 1);
            ruleMapping.Add((false, false, false), binDigits[7] == 1);
            return ruleMapping;
        }
        static void PrintBoard(bool[] board) => Console.WriteLine(string.Join("", board.Select(val => val ? "#" : " ")));
        static void Main(string[] args)
        {
            byte rule = 110;
            int boardSize = Console.WindowWidth;
            int iterations = 1_000_000;
            int fps = 24;
            bool randomStart = false;

            // getting the values from the console
            try
            {
                byte.TryParse(args[0], out byte newRule);
                if (newRule is not default(byte)) rule = newRule;
                int.TryParse(args[1], out int newBoardSize);
                if (newBoardSize is not default(int)) boardSize = newBoardSize;
                int.TryParse(args[2], out int newIterations);
                if (newIterations is not default(int)) iterations = newIterations;
                int.TryParse(args[3], out int newFps);
                if (newFps is not default(int)) fps = newFps;
                bool.TryParse(args[4], out randomStart);
            }
            catch (Exception)
            {
            }

            var ruleMapping = GetRuleMapping(rule);
            var startingBoard = GetStartingBoard(boardSize, randomStart);
            while (iterations-- > 0)
            {
                PrintBoard(startingBoard);
                startingBoard = SolveBoard(startingBoard, ruleMapping);
                System.Threading.Thread.Sleep(1000 / fps);
            }
        }
    }
}
