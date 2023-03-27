using DiDemo.Data;
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

namespace DiDemo.Views
{
    /// <summary>
    /// DataBindingBookPage.xaml 的交互逻辑
    /// </summary>
    public partial class DataBindingBookPage : Page
    {
        private BookInfo book;

        public DataBindingBookPage()
        {
            InitializeComponent();
            book = new BookInfo
            {
                Id = 1,
                Title = "Title",
                Description = "Description",
                Author = "Author",
            };
            DataContext = book;

            Validation.AddErrorHandler(vrtb2, (s, e) =>
            {
                vrtb1.Text = "aaaaa";
                vrtb2.ToolTip = e.Error.ErrorContent.ToString();
            });
        }
    }
}
