using ConsoleSnakeGame.Elements;

namespace ConsoleSnakeGame.Display
{
    public abstract class Screen : IScreen
    {
        public int Height { get; }
        public int Width { get; }
        public Screen(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public Point GetCenter() => new Point(Width / 2, Height / 2);
        public abstract void SetPixel(Point point);
        public abstract void ClearPixel(Point point);
    }
}
