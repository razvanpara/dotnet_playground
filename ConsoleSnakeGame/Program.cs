using ConsoleSnakeGame.Display;
using System;
using System.Threading.Tasks;

namespace ConsoleSnakeGame
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter color:");
            Console.WriteLine("Options:\n1 - color\n0 - black and white");
            int screenWidth = 60;
            int screenHeight = 20;
            IScreen gameScreen = null;
            int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int option);
            Console.Clear();
            Console.CursorVisible = false;
            gameScreen = (option == 1) ? new ColorScreen(screenWidth, screenHeight, ConsoleColor.White) : new BWScreen(screenWidth, screenHeight);
            var game = new SnakeGame(gameScreen);
            await game.Play();
        }
    }
}
