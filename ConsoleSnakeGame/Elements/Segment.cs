using System;
using System.Collections.Generic;

namespace ConsoleSnakeGame.Elements
{
    public class Segment
    {
        public Segment(ConsoleColor[,] pixels)
        {
            Pixels = pixels;
        }

        public ConsoleColor[,] Pixels { get; }

        public IEnumerable<Point> GetPoints(int x, int y)
        {
            var list = new List<Point>();
            var length = Pixels.GetLength(0);
            for (int r = 0; r < length; r++)
                for (int c = 0; c < length; c++)
                    list.Add(new Point(c + x, r + y, Pixels[r, c]));
            return list;
        }
    }

}
