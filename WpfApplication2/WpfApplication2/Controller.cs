using System;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media;

namespace Wpf_SNAKE
{
    class Controller
    {

        public int BoardHeight { get; set; }
        public int BoardWidth { get; set; }
        public int PointWidth { get; set; }

        private Random random = new Random();
        private Point hitpoint;

        private List<Point> pointcollection = new List<Point>();

        private Snake snakeman;

        public Controller(int BoardHeight, int BoardWidth, int PointWidth)
        {
            this.BoardHeight = BoardHeight;
            this.BoardWidth = BoardWidth;
            this.PointWidth = PointWidth;
            hitpoint = null;
            snakeman = new Snake(10, 10, PointWidth);
            AddPointNormal(1);
            AddPointSpeed(1);
        }

        private action Splitter(action snake, action global)
        {
            if (snake == action.ok && global == action.ok) return action.ok;
            else if (snake == action.die || global == action.die) return action.die;
            else if (snake == action.ok && global == action.eat) return action.eat;
            else return action.die;
        }

        private action CheckGlobalCollision(int x_point, int y_point)
        {
            action tmp = action.ok;

            foreach (var point in pointcollection)
            {
                if (x_point == point.X_value && y_point == point.Y_value)
                {
                    tmp = action.eat;
                    hitpoint = point;
                }

            }

            if (x_point <= 0 || x_point >= Convert.ToInt16(BoardWidth) ||
                y_point <= 0 || y_point >= Convert.ToInt16(BoardHeight))
            {
                tmp = action.die;
            }

            return tmp;
        }

        public action UpdateElements(Direction Direction, Canvas board)
        {
            int X_head = 0;
            int Y_head = 0;

            int slenght=snakeman.BodyInformation(0, ref X_head, ref Y_head);

            action tmp = Splitter(snakeman.CheckCollision(true), CheckGlobalCollision(X_head, Y_head));

            if (tmp != action.die)
            {

                snakeman.BodyMove(Direction);

                snakeman.PrintSnake(board);

                foreach (var ppoint in pointcollection)
                {
                    Rectangle point = new Rectangle();
                    point.Height = ppoint.Height;
                    point.Width = ppoint.Height;
                    point.Fill = ppoint.Color;
                    Canvas.SetLeft(point, ppoint.X_value);
                    Canvas.SetTop(point, ppoint.Y_value);
                    board.Children.Add(point);
                }
            }

            return tmp;

        }

        public action Whatpoint()
        {
            pointcollection.Remove(hitpoint);
            return hitpoint.Action();
        }

        public void WherePutNewSnakePoint()
        {
            int X_head = 0;
            int Y_head = 0;

            int slenght = snakeman.BodyInformation(0, ref X_head, ref Y_head);
            int tmp = snakeman.BodyInformation(slenght-1, ref X_head, ref Y_head);


            if (Splitter(snakeman.CheckCollision(X_head - PointWidth, Y_head), CheckGlobalCollision(X_head - PointWidth, Y_head))==action.ok)
            {
                snakeman.BodyIcrease(X_head - PointWidth, Y_head);
            }
            else if (Splitter(snakeman.CheckCollision(X_head + PointWidth, Y_head), CheckGlobalCollision(X_head + PointWidth, Y_head)) == action.ok)
            {
                snakeman.BodyIcrease(X_head + PointWidth, Y_head);
            }
            else if (Splitter(snakeman.CheckCollision(X_head, Y_head - PointWidth), CheckGlobalCollision(X_head, Y_head - PointWidth)) == action.ok)
            {
                snakeman.BodyIcrease(X_head, Y_head - PointWidth);
            }
            else if (Splitter(snakeman.CheckCollision(X_head, Y_head + PointWidth), CheckGlobalCollision(X_head, Y_head + PointWidth)) == action.ok)
            {
                snakeman.BodyIcrease(X_head, Y_head + PointWidth);
            }
            else 
            {
            }

            AddPointNormal(2);

        }

        private void Generigxy(ref int X, ref int Y)
        {
            do
            {
                X = random.Next(1, Convert.ToInt16(BoardWidth / PointWidth));
                Y = random.Next(1, Convert.ToInt16(BoardHeight / PointWidth));

            } while ((snakeman.CheckCollision(X * PointWidth, Y * PointWidth) != action.ok) && (CheckGlobalCollision(X * PointWidth, Y * PointWidth) != action.ok));

        }

        public void AddPointNormal(int lenght)
        {

            int X = 0;
            int Y = 0;
            for (int i = 0; i < lenght; i++)
            {
                Generigxy(ref X, ref Y);
                pointcollection.Add(new Point_normal() { X_value = X * PointWidth, Y_value = Y * PointWidth, Color = Brushes.Green, Height = PointWidth });
            }

        }

        public void AddPointSpeed(int lenght)
        {

            int X = 0;
            int Y = 0;
            for (int i = 0; i < lenght; i++)
            {
                Generigxy(ref X, ref Y);
                pointcollection.Add(new Point_speed() { X_value = X * PointWidth, Y_value = Y * PointWidth, Color = Brushes.Yellow, Height = PointWidth });
            }

        }

    }
}
