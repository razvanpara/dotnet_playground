using ConsoleSnakeGame.Elements;
using System;

namespace ConsoleSnakeGame.Display
{
    public class ColorScreen : BWScreen
    {
        ConsoleColor _background;
        public ColorScreen(int width, int height, ConsoleColor color) : base(width, height)
        {
            _background = color;
            SetBackground();
        }
        public ColorScreen(int width, int height) : base(width, height)
        {
            SetBackground();
        }

        private void SetBackground()
        {
            Console.BackgroundColor = _background;
            for (int c = 0; c < Width; c++)
                for (int r = 0; r < Height; r++)
                    base.ClearPixel(new Point(c, r));
        }
        private void ResetColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override void ClearPixel(Point point)
        {
            Console.BackgroundColor = _background;
            base.ClearPixel(point);
            ResetColor();
        }
        public override void SetPixel(Point point)
        {
            Console.BackgroundColor = _background;
            Console.ForegroundColor = point.Color;
            base.SetPixel(point);
            ResetColor();
        }
    }
}
