using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Wpf_SNAKE
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public enum action
    {
        ok,
        speed,
        eat,
        die
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Direction keyboardkey = Direction.Down;
        private int snakespeed = 100;
        private int elementheight = 10;
        private int totalscore = 0;
        private bool startgame = true;

        private DispatcherTimer countertime = new DispatcherTimer();
        private Controller boardcontroller;


        public MainWindow()
        {
            InitializeComponent();

            countertime.Tick += new EventHandler(Countertime_event);
            countertime.Interval = new TimeSpan(0, 0, 0, 0, snakespeed);

            textBox1.Visibility = Visibility.Hidden;
            textBox2.Visibility = Visibility.Visible;
            textBox3.Visibility = Visibility.Visible;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                keyboardkey = Direction.Up;
            }

            if (e.Key == Key.Down)
            {
                keyboardkey = Direction.Down;
            }

            if (e.Key == Key.Left)
            {
                keyboardkey = Direction.Left;
            }

            if (e.Key == Key.Right)
            {
                keyboardkey = Direction.Right;
            }

            if ((e.Key == Key.Space) && startgame)
            {
                textBox1.Visibility = Visibility.Hidden;
                textBox2.Visibility = Visibility.Hidden;
                textBox3.Visibility = Visibility.Hidden;

                keyboardkey = Direction.Down;
                totalscore = 0;
                snakespeed = 100;
                countertime.Interval = new TimeSpan(0, 0, 0, 0, snakespeed);
                countertime.Start();

                boardcontroller = new Controller(Convert.ToInt16(board.ActualHeight),
                                                 Convert.ToInt16(board.ActualWidth),
                                                 elementheight
                );


                startgame = false;
            }
        }

        private void Countertime_event(object sender, EventArgs e)
        {

            if (textBox2.Visibility == Visibility.Hidden) board.Children.Clear();
            else board.UpdateLayout();
            score_int.Content = totalscore.ToString();

            switch (boardcontroller.UpdateElements(keyboardkey, board))
            {
                case action.ok: break;
                case action.die:
                    countertime.Stop();
                    startgame = true;
                    textBox1.Visibility = Visibility.Visible;
                    textBox2.Visibility = Visibility.Visible;
                    textBox3.Visibility = Visibility.Visible;
                    break;
                case action.eat:
                    switch (boardcontroller.Whatpoint())
                    {
                        case action.speed:
                            totalscore += 10;
                            if (snakespeed > 5) snakespeed -=5;
                            countertime.Stop();
                            countertime.Interval = new TimeSpan(0, 0, 0, 0, snakespeed);
                            countertime.Start();
                            boardcontroller.AddPointSpeed(2);
                            break;
                        case action.eat:
                            totalscore += 1;
                            boardcontroller.WherePutNewSnakePoint();
                            break;
                        default: break;
                    }
                    break;
                default: break;
            }

        }

    }

}
