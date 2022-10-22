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
using System.ComponentModel;

namespace HaloKid.Modules
{
    class StudentPageModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string avatar = null;

        public string Avatar
        {
            get { return avatar; }
            set
            {
                avatar = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Avatar"));
            }
        }
    }

    /// <summary>
    /// StudentPanel.xaml 的交互逻辑
    /// </summary>
    public partial class StudentPanel : Page
    {
        public StudentPanel()
        {
            InitializeComponent();
        }
    }
}
