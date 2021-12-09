using ConsoleSnakeGame.Display;
using ConsoleSnakeGame.Elements;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleSnakeGame
{
    public class SnakeGame
    {
        private IScreen _screen;
        private Point _food;
        private Snake _snake;
        private Random _rand = new Random();
        private Queue<ConsoleKey> _commands;

        public SnakeGame(IScreen screen)
        {
            _screen = screen;
            _snake = Snake.GetStartingSnake(_screen);
            _commands = new Queue<ConsoleKey>();
        }

        private async Task PrintNextFrame(ConsoleKey direction)
        {
            if (_snake.SetMovingDirection(direction))
            {
                RemoveSnake();
                _snake.MoveSnakeHead(_screen);
                if (_snake.Head == _food) SpawnNewFood();
                else _snake.CutSnakeTail();

                UpdateSnake();
                UpdateFood();
                await Task.Delay(1000 / _snake.Segments.Count);
            }
        }
        private void RemoveSnake()
        {
            foreach (var segment in _snake.Segments)
                _screen.ClearPixel(segment);
        }
        private void UpdateFood() => _screen.SetPixel(_food);
        private void UpdateSnake()
        {
            foreach (var segment in _snake.Segments)
                _screen.SetPixel(segment);
        }
        private void SpawnNewFood()
        {
            var newFood = new Point(_rand.Next(0, _screen.Width), _rand.Next(0, _screen.Height));
            while (newFood == _food || _snake.IsSnakeSegment(newFood))
                newFood = new Point(_rand.Next(0, _screen.Width), _rand.Next(0, _screen.Height));
            newFood.Color = ConsoleColor.Red;
            newFood.Symbol = '#';
            _food = newFood;
        }

        public async Task Play()
        {
            ConsoleKey direction = ConsoleKey.RightArrow;
            SpawnNewFood();
            do
            {
                if (Console.KeyAvailable) _commands.Enqueue(Console.ReadKey(true).Key);
                if (_commands.Count > 0) direction = _commands.Dequeue();
                await PrintNextFrame(direction);
            } while (_snake.Alive && direction != ConsoleKey.Escape);
            Console.WriteLine($"Game over!\n Score: {_snake.Segments.Count - 3}");
        }
    }
}
