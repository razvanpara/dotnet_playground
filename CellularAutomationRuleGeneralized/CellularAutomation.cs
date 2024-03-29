﻿using CellularAutomationRuleGeneralized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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
        public static bool[] SolveBoard(BigInteger rule, int ruleSpace, bool[] board)
        {
            var ruleMapping = GetRuleMapping(rule, ruleSpace);
            var length = board.Length;
            var newBoard = new bool[length];

            for (int i = 0; i < newBoard.Length; i++)
                newBoard[i] = GetCellNextValue(i, ruleSpace, board, ruleMapping);
            return newBoard;
        }
        public static void PrintBoard(bool[] board) => Console.WriteLine(string.Join("", board.Select(val => val ? "#" : " ")));
        public static void PrintRuleMatrix(BigInteger rule, int ruleSpace)
        {
            var rules = GetRuleMapping(rule, ruleSpace).OrderBy(kvp => int.Parse(string.Join("", kvp.Key.Split(" ").Select(i => bool.Parse(i) ? "1" : "0"))));
            Console.WriteLine(string.Format("rule {0} in {1}bit space => {2}", rule, ruleSpace, string.Join(" ", rules.Select(kvp => kvp.Value ? "1" : "0").Reverse())));
            Console.WriteLine();
            rules.ToList().ForEach(r => Console.WriteLine(string.Format("{0,-55} => {1}", r.Key, r.Value)));
        }

        private static bool[] GetNumberAsDigitArray(BigInteger number, int minDigits)
        {
            var numberBinaryArray = BigIntConversion.ToBase2(number).ToCharArray().Select(c => c == '1');
            //var leadingZeroes = Enumerable.Repeat(false, minDigits - numberBinaryArray.Count());
            //leadingZeroes.ToList().ForEach(zero => numberBinaryArray = numberBinaryArray.Prepend(zero));
            //

            // since using big integers, we can no longer use the above code
            // with big integers, the lowest array lenght is 8 so in order to get just the bits we need
            // we skip the first array.length - minimum digits
            if (minDigits > numberBinaryArray.Count())
                Enumerable.Repeat(false, minDigits - numberBinaryArray.Count()).ToList().ForEach(b => numberBinaryArray = numberBinaryArray.Prepend(b));
            return numberBinaryArray.Skip(numberBinaryArray.Count() - minDigits).ToArray();
        }
        private static Dictionary<string, bool> GetRuleMapping(BigInteger rule, int ruleSpace)
        {
            var ruleMapping = new Dictionary<string, bool>();
            var ruleArr = GetNumberAsDigitArray(rule, ruleSpace);

            // we're taking from the rule's binary digits, just the digits that fit our rule space
            // this is only relevant if we need fewer digits from the main rule when we generate the smaller rules that actually apply to cells
            // example:
            // we have 16238 as a rule which has the binary representation 11111101101110
            // if we're running this with a rule space of 8, we will only take the last 8 digits from the binary represenation, meaning 01101110
            // which will be the same as running with the rule 110 which represented in binary is also 01101110
            ruleArr = ruleArr.Skip(ruleArr.Length - ruleSpace).ToArray();

            for (var i = 0; i < ruleArr.Length; i++)
            {
                var subRuleDigitsCount = Convert.ToString(ruleSpace - 1, 2).Length;
                var key = string.Join(" ", GetNumberAsDigitArray(new BigInteger(i), subRuleDigitsCount));
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
