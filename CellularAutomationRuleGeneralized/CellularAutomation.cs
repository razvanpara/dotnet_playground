using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomationRule16bit
{
    public class CellularAutomation
    {
        public static bool[] GetStartingBoard(int boardSize, bool randomStart = false)
        {
            var random = new Random();
            var board = new bool[boardSize];
            board[boardSize / 2] = true;
            if (randomStart)
                board = board.Select(b => random.Next(0, 2) == 1).ToArray();
            return board;
        }
        public static bool[] SolveBoard(long rule, int ruleSpace, bool[] board)
        {
            var ruleMapping = GetRuleMapping(rule, ruleSpace);
            var length = board.Length;
            var newBoard = new bool[length];

            for (int i = 0; i < newBoard.Length; i++)
                newBoard[i] = GetCellNextValue(i, ruleSpace, board, ruleMapping);
            return newBoard;
        }
        public static void PrintBoard(bool[] board) => Console.WriteLine(string.Join("", board.Select(val => val ? "#" : " ")));
        public static void PrintRuleMatrix(long rule, int ruleSpace)
        {
            var rules = GetRuleMapping(rule, ruleSpace).OrderBy(kvp => int.Parse(string.Join("", kvp.Key.Split(" ").Select(i => bool.Parse(i) ? "1" : "0"))));
            Console.WriteLine(string.Format("rule {0} in {1}bit space => {2}", rule, ruleSpace, string.Join(" ", rules.Select(kvp => kvp.Value ? "1" : "0").Reverse())));
            Console.WriteLine();
            rules.ToList().ForEach(r => Console.WriteLine(string.Format("{0,-55} => {1}", r.Key, r.Value)));
        }

        private static bool[] GetNumberAsDigitArray(long number, int minDigits)
        {
            var numberBinaryArray = Convert.ToString(number, 2).ToCharArray().Select(c => c == '1'); ;
            var leadingZeroes = Enumerable.Repeat(false, minDigits - numberBinaryArray.Count());
            leadingZeroes.ToList().ForEach(zero => numberBinaryArray = numberBinaryArray.Prepend(zero));
            return numberBinaryArray.ToArray();
        }
        private static Dictionary<string, bool> GetRuleMapping(long rule, int ruleSpace)
        {
            var ruleMapping = new Dictionary<string, bool>();
            var ruleArr = GetNumberAsDigitArray(rule, ruleSpace);
            for (var i = 0; i < ruleArr.Length; i++)
            {
                var subRuleDigitsCount = Convert.ToString(ruleSpace - 1, 2).Length;
                var key = string.Join(" ", GetNumberAsDigitArray(i, subRuleDigitsCount));
                ruleMapping[key] = ruleArr[^(i + 1)];
            }
            return ruleMapping;
        }
        private static int[] GetIndexes(int index, int ruleSpace, int boardSize)
        {
            int ruleSize = Convert.ToString((ruleSpace - 1), 2).Length;
            if (index == 0)
                return Enumerable.Range(-1, ruleSize).Select(i => i < 0 ? boardSize - 1 : i % boardSize).ToArray();
            return Enumerable.Range(index - 1, ruleSize).Select(i => i % boardSize).ToArray();
        }
        private static string GetKeyFromBoard(int index, int ruleSpace, bool[] board)
        {
            var indexes = GetIndexes(index, ruleSpace, board.Length);
            return string.Join(" ", indexes.Select(i => board[i]));
        }
        private static bool GetCellNextValue(int index, int ruleSpace, bool[] board, Dictionary<string, bool> ruleMapping)
        {
            var key = GetKeyFromBoard(index, ruleSpace, board);
            return ruleMapping[key];
        }
    }
}
