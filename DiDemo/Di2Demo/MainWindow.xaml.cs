using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Di2Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ILogger<MainWindow> logger;
        private CancellationTokenSource actionCts;

        public MainWindow(ILogger<MainWindow> logger)
        {
            this.logger = logger;
            actionCts = new CancellationTokenSource();
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var ts = TaskScheduler.FromCurrentSynchronizationContext();
            await Task.Delay(2000).ConfigureAwait(false);
            await Task.Factory.StartNew(() =>
            {
                var s = CounterLabel.Content as string;
                logger.LogInformation("count: {0} {1}", CounterLabel.Content, s);
                if (!string.IsNullOrEmpty(s))
                {
                    var i = int.Parse(s!);
                    CounterLabel.Content = (++i).ToString();
                }
            }, actionCts.Token, TaskCreationOptions.None, ts);
        }
    }
}
