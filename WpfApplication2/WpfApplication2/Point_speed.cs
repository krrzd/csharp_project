namespace Wpf_SNAKE
{
    class Point_speed : Point
    {
        static int numerofinstances { get; set; } = 1;

        override public action Action()
        {
            numerofinstances++;
            return action.speed;
        }

    }
}
