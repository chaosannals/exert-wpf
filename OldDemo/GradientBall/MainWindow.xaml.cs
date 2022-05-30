using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GradientBall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isMoving = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void onMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isMoving = false;
            var start = DateTime.Now;

            DragMove();

            var end = DateTime.Now;
            Debug.WriteLine("{0}ms", (end - start).TotalMilliseconds);
            int timestamp = e.Timestamp + (int)(end - start).TotalMilliseconds;
            RaiseEvent(new MouseButtonEventArgs(Mouse.PrimaryDevice, timestamp, MouseButton.Left)
            {
                RoutedEvent = UIElement.MouseUpEvent,
                Source = sender,
            });
        }

        private void onMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!isMoving)
            {
                Debug.WriteLine("Left Button Up");
            }
            isMoving = false;
        }

        private void onLocationChanged(object sender, EventArgs e)
        {
            isMoving = true;
            Point p = PointToScreen(new Point());
            Debug.WriteLine("({0}, {1})", p.X, p.Y);
        }
    }
}
