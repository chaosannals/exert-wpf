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

namespace HaloKid.Pages
{
    /// <summary>
    /// IndexPage.xaml 的交互逻辑
    /// </summary>
    public partial class IndexPage : Page
    {
        public IndexPage()
        {
            InitializeComponent();
        }

        public void SwitchPanel(string name)
        {
            string path = string.Format("pack://application:,,,/Modules/{0}Panel.xaml", name);
            ContentPanel.Navigate(new Uri(path));
        }

        private void onClickTeacherItem(object sender, MouseButtonEventArgs e)
        {
            SwitchPanel("Teacher");
        }

        private void onClickStudentItem(object sender, MouseButtonEventArgs e)
        {
            SwitchPanel("Student");
        }
    }
}
