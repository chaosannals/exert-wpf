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
using HaloKid.Models;

namespace HaloKid.Pages
{
    /// <summary>
    /// 模型
    /// </summary>
    public class LoginPageModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string username = string.Empty;
        private string password = string.Empty;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Username"));
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
            }
        }
    }

    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPageModel Model { get; private set; }
        public LoginPage()
        {
            Model = new LoginPageModel();
            InitializeComponent();
            DataContext = this;
        }

        private void onClickSubmit(object sender, RoutedEventArgs e)
        {
            using (HkDbc dbc = new HkDbc())
            {
                AccountModel ac = (from a in dbc.Set<AccountModel>()
                                   where a.Account == Model.Username
                                   select a).FirstOrDefault();
                if (ac == null)
                {
                    return;
                }
                string pass = PassBox.Password.ToSha256();
                if (ac.Password != pass)
                {
                    return;
                }
            }
            MainWindow window = Window.GetWindow(this) as MainWindow;
            window.SwitchPage("Index");
        }
    }
}
