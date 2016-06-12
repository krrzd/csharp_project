using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Wpf_SNAKE
{

    class Snake : IAnimal
    {
        protected List<Point_normal> Body = new List<Point_normal>();

        public Brush Color { get; set; } = Brushes.Green;
        public int Radius { get; set; }
        public int X_start { get; set; }
        public int Y_start { get; set; }

        public Snake(int X_start, int Y_start, int Radius)
        {
            this.Radius = Radius;
            this.X_start = X_start;
            this.Y_start = Y_start;

            Point_normal head = new Point_normal() { X_value = X_start, Y_value = Y_start, Color = Brushes.Blue, Height = Radius };
            Body.Add(head);
        }

        public void BodyIcrease(int X_value, int Y_value)
        {
            Point_normal body_point = new Point_normal() { X_value = X_value, Y_value = Y_value, Color = Color, Height = Radius };
            Body.Add(body_point);
        }

        public void BodyMove(Direction direction)
        {
            for (int i = Body.Count; i > 1; i--)
            {
                Body[i - 1].X_value = Body[i - 2].X_value;
                Body[i - 1].Y_value = Body[i - 2].Y_value;
            }

            if (direction == Direction.Down)
            {
                Body[0].Y_value = Body[0].Y_value + Radius;
            }

            if (direction == Direction.Up)
            {
                Body[0].Y_value = Body[0].Y_value - Radius;
            }

            if (direction == Direction.Left)
            {
                Body[0].X_value = Body[0].X_value - Radius;
            }

            if (direction == Direction.Right)
            {
                Body[0].X_value = Body[0].X_value + Radius;
            }


        }

        public action CheckCollision(int x_point, int y_point)
        {

            action tmp = action.ok;

            foreach (var point in Body)
            {
                if (x_point == point.X_value && y_point == point.Y_value)
                {
                    tmp = action.die;
                }
            }

            return tmp;

        }

        public action CheckCollision(bool head)
        {

            action tmp = action.ok;
            int x_point = Body[0].X_value;
            int y_point = Body[0].Y_value;
            int ishead = 1;

            foreach (var point in Body)
            {
                if (x_point == point.X_value && y_point == point.Y_value)
                {
                    if (ishead == 1) ishead--;
                    else tmp = action.die;
                }
            }

            return tmp;

        }

        public int BodyInformation(int index, ref int X_point, ref int Y_point)
        {

            try
            {
                X_point = Body[index].X_value;
                Y_point = Body[index].Y_value;
            }
            catch (System.ArgumentOutOfRangeException e)
            {

                X_point = Body[Body.Count-1].X_value;
                Y_point = Body[Body.Count-1].Y_value;
            }


            return Body.Count;
        }

        public void PrintSnake(Canvas board)
        {

            foreach (var snakebodypoint in Body)
            {
                Ellipse point = new Ellipse();
                point.Height = snakebodypoint.Height;
                point.Width = snakebodypoint.Height;
                point.Fill = snakebodypoint.Color;
                Canvas.SetLeft(point, snakebodypoint.X_value);
                Canvas.SetTop(point, snakebodypoint.Y_value);
                board.Children.Add(point);
            }

        }

    }
}
