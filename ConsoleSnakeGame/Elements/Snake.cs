using ConsoleSnakeGame.Display;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleSnakeGame.Elements
{
    public class Snake
    {
        private ConsoleKey _direction = ConsoleKey.RightArrow;
        public bool Alive { get; private set; }
        public LinkedList<Point> Segments { get; private set; }
        public Point Head { get => Segments.First.Value; }
        public ConsoleKey MovingDirection { get => _direction; }
        private ConsoleColor _mouthColor = ConsoleColor.Red;
        private ConsoleColor _headColor = ConsoleColor.Black;
        private ConsoleColor _bodyColor = ConsoleColor.Black;

        private Snake(params Point[] segments)
        {
            Segments = new LinkedList<Point>(segments);
            Alive = true;
        }
        public static Snake GetStartingSnake(IScreen screen)
        {
            var snakeHead = screen.GetCenter();
            var snakeBody = new Point(snakeHead.X - 1, snakeHead.Y);
            var snakeTail = new Point(snakeHead.X - 2, snakeHead.Y);
            var snake = new Snake(snakeHead, snakeBody, snakeTail);
            snake.SetBodyStyle();
            return snake;
        }
        public bool SetMovingDirection(ConsoleKey direction)
        {
            if (IsValidDirection(direction))
            {
                if (!AreOposingDirections(_direction, direction))
                    _direction = direction;
                return true;
            }
            return false;
        }
        public void MoveSnakeHead(IScreen screen)
        {
            MoveSegments(screen);
            SetBodyStyle();
        }
        public void CutSnakeTail() => Segments.RemoveLast();
        public bool IsSnakeSegment(Point point)
        {
            return Segments.Any(seg => seg == point);
        }

        private void SetBodyStyle()
        {
            // setting mouth
            var bodySegment = Segments.First;
            bodySegment.Value.Color = _mouthColor;
            bodySegment.Value.Symbol = GetMouthDirection(_direction);
            // setting head
            bodySegment = bodySegment.Next;
            bodySegment.Value.Color = _headColor;
            bodySegment.Value.Symbol = 'O';

            //setting the rest of the body
            bodySegment = bodySegment.Next;
            while (bodySegment != null)
            {
                bodySegment.Value.Color = _bodyColor;
                bodySegment.Value.Symbol = 'o';
                bodySegment = bodySegment.Next;
            }
        }
        private char GetMouthDirection(ConsoleKey direction)
        {
            return direction switch
            {
                ConsoleKey.UpArrow => 'v',
                ConsoleKey.DownArrow => '^',
                ConsoleKey.LeftArrow => '>',
                _ => '<'
            };
        }
        private bool IsValidDirection(ConsoleKey newDirection)
        {
            var directions = new ConsoleKey[] { ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow };
            return directions.Any(d => d == newDirection);
        }
        private bool AreOposingDirections(ConsoleKey d1, ConsoleKey d2)
        {
            return Math.Abs(d1 - d2) == 2;
        }
        private void MoveSegments(IScreen screen)
        {
            var nextPoint = GetNextPoint(screen);
            if (IsSnakeSegment(nextPoint))
            {
                Alive = false;
                return;
            }
            Segments.AddFirst(nextPoint);

        }
        private Point GetNextPoint(IScreen screen)
        {
            var point = Segments.First.Value;
            var pointX = point.X;
            var pointY = point.Y;
            switch (_direction)
            {
                case ConsoleKey.UpArrow:
                    pointY--;
                    break;
                case ConsoleKey.DownArrow:
                    pointY++;
                    break;
                case ConsoleKey.LeftArrow:
                    pointX--;
                    break;
                default:
                    pointX++;
                    break;
            }
            pointX = pointX < 0 ? screen.Width + pointX : pointX % screen.Width;
            pointY = pointY < 0 ? screen.Height + pointY : pointY % screen.Height;
            return new Point(pointX, pointY, ConsoleColor.DarkGreen);
        }
    }
}
