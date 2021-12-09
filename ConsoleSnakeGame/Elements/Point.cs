using System;

namespace ConsoleSnakeGame.Elements
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public ConsoleColor Color { get; set; }
        public char Symbol { get; set; } = ' ';

        public Point(int x, int y, ConsoleColor color, char symbol) : this(x, y, color)
        {
            Symbol = symbol;
        }

        public Point(int x, int y, ConsoleColor color) : this(x, y)
        {
            Color = color;
        }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override bool Equals(object obj) => (obj is Point p) ? X == p.X && Y == p.Y : false;
        public override int GetHashCode() => base.GetHashCode();
        public static bool operator ==(Point p1, Point p2) => p1.Equals(p2);
        public static bool operator !=(Point p1, Point p2) => !(p1 == p2);
    }

}
