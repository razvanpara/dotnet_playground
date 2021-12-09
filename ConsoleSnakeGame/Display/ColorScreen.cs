using ConsoleSnakeGame.Elements;
using System;

namespace ConsoleSnakeGame.Display
{
    public class ColorScreen : BWScreen
    {
        public ColorScreen(int width, int height) : base(width, height)
        {

        }

        private void ResetColor() => Console.ForegroundColor = ConsoleColor.White;
        public override void SetPixel(Point point)
        {
            Console.ForegroundColor = point.Color;
            base.SetPixel(point);
            ResetColor();
        }
    }
}
