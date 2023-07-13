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

namespace Di4NavDemo.Pages
{
    /// <summary>
    /// IndexPage.xaml 的交互逻辑
    /// </summary>
    public partial class IndexPage : Page
    {
        private ErrorPageFunction errorPage;

        public IndexPage(ErrorPageFunction errorPage)
        {
            InitializeComponent();
            this.errorPage = errorPage;
        }

        private void onClickToAbout(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate("./Pages/AboutPage.xaml");
            NavigationService.Navigate(errorPage);
        }
    }
}
