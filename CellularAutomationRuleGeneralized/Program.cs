using System;
using System.Numerics;

namespace CellularAutomationRule16bit
{
    class Program
    {
        static ConsoleColor[] _colors = new ConsoleColor[]
        {
            ConsoleColor.DarkBlue,
            ConsoleColor.DarkGreen,
            ConsoleColor.DarkCyan,
            ConsoleColor.DarkRed,
            ConsoleColor.DarkMagenta,
            ConsoleColor.DarkYellow,
            ConsoleColor.Gray,
            ConsoleColor.DarkGray,
            ConsoleColor.Blue,
            ConsoleColor.Green,
            ConsoleColor.Cyan,
            ConsoleColor.Red,
            ConsoleColor.Magenta,
            ConsoleColor.Yellow,
            ConsoleColor.White
        };
        static void Main(string[] args)
        {
            var rule = new BigInteger(110);
            var ruleSpace = 8; // bit
            var boardSize = 200;
            var iterations = 100;
            var fps = 24;
            var randomColors = false;

            Console.WriteLine("Enter rule: (defaults to 110)");
            var input = Console.ReadLine();
            if (input is not "" && BigInteger.TryParse(input, out BigInteger newRule))
                if (newRule > 0)
                    rule = newRule;

            Console.WriteLine("Enter rule space: (defaults to 8bit)");
            input = Console.ReadLine();
            if (input is not "" && int.TryParse(input, out int newSpace))
                //if (newSpace > 8 && newSpace <= 64) 
                if (newSpace > 8)
                    ruleSpace = newSpace;

            Console.WriteLine("Enter board size: (defaults to 200 columns)");
            input = Console.ReadLine();
            if (input is not "" && int.TryParse(input, out int newBoardSize))
                if (newBoardSize > 0)
                    boardSize = newBoardSize;

            Console.WriteLine("Enter iterations count: (defaults 100)");
            input = Console.ReadLine();
            if (input is not "" && int.TryParse(input, out int newIterations))
                if (newIterations > 0)
                    iterations = newIterations;

            Console.WriteLine("Enter fps: (defaults to 24, max 144)");
            input = Console.ReadLine();
            if (input is not "" && int.TryParse(input, out int newFps))
                if (newFps > 0 && fps <= 144)
                    fps = newFps;

            Console.WriteLine("Random colours ? (1 for true, 0 for false, defaults to false)");
            input = Console.ReadLine();
            randomColors = input == "1";




            var board = CellularAutomation.GetStartingBoard(boardSize);
            CellularAutomation.PrintRuleMatrix(rule, ruleSpace);
            for (int i = 0; i < iterations; i++)
            {
                if (randomColors)
                    Console.ForegroundColor = _colors[i % _colors.Length];
                CellularAutomation.PrintBoard(board);
                board = CellularAutomation.SolveBoard(rule, ruleSpace, board);
                System.Threading.Thread.Sleep(1000 / fps);
            }
        }
    }
}