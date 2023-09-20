using Di4NavDemo.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Di4NavDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IndexPage indexPage;

        public MainWindow(IndexPage indexPage)
        {
            InitializeComponent();
            this.indexPage = indexPage;
            RouteBox.NavigationService.Navigate(indexPage);
        }
    }
}
