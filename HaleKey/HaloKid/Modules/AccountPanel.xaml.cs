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

namespace HaloKid.Modules
{
    public class AccountPanelModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private AccountModel info;
        
        public AccountModel Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Info"));
            }
        }
    }

    /// <summary>
    /// AccountPage.xaml 的交互逻辑
    /// </summary>
    public partial class AccountPanel : Page
    {
        public AccountPanelModel Model { get; private set; }
        public List<AccountModel> Accounts { get; private set; }
        public int PageCount { get { return (int)Math.Ceiling((double)Accounts.Count / 20); } }
        public AccountPanel()
        {
            using (HkDbc dbc = new HkDbc())
            {
                Model = new AccountPanelModel{};
                Accounts = (from a in dbc.Set<AccountModel>() select a).ToList();
            }
            InitializeComponent();
            DataContext = this;
        }
    }
}
