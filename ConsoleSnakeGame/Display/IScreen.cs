using ConsoleSnakeGame.Elements;

namespace ConsoleSnakeGame.Display
{
    public interface IScreen
    {
        int Width { get; }
        int Height { get; }
        Point GetCenter();
        void SetPixel(Point point);
        void ClearPixel(Point point);
    }
}
