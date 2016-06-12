using System.Windows.Media;

namespace Wpf_SNAKE
{

    abstract class Point
    {

        public int X_value { get; set; }
        public int Y_value { get; set; }
        public Brush Color { get; set; } = Brushes.Black;
        public int Height { get; set; }
        public int Width { get; set; }
        public Brush Stroke { get; set; } = Brushes.Black;

        virtual public action Action()
        {
            return action.ok;
        }

    }
}
