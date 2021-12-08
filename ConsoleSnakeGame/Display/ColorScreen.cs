﻿using ConsoleSnakeGame.Elements;
using System;

namespace ConsoleSnakeGame.Display
{
    public class ColorScreen : Screen
    {
        public ColorScreen(int width, int height) : base(width, height)
        {

        }

        private void ResetColor() => Console.BackgroundColor = ConsoleColor.Black;
        private void ResetCursor() => Console.SetCursorPosition(0, Height);
        private void MoveCursorTo(Point point) => Console.SetCursorPosition(point.X, point.Y);

        public override void ClearPixel(Point point)
        {
            MoveCursorTo(point);
            Console.Write(" ");
            ResetCursor();
        }
        public override void SetPixel(Point point)
        {
            MoveCursorTo(point);
            Console.BackgroundColor = point.Color;
            Console.Write(" ");
            ResetCursor();
            ResetColor();
        }
    }
}
