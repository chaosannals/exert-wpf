using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace DiDemo.Views
{
    /// <summary>
    /// ButtonTemplatePage.xaml 的交互逻辑
    /// </summary>
    public partial class ButtonTemplatePage : Page
    {
        public ButtonTemplatePage()
        {
            InitializeComponent();
            ReadDefaultButtonTemplate();
        }

        private void ReadDefaultButtonTemplate()
        {
            var button = new Button();
            button.Content = "test";
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = new string(' ', 4);
            settings.NewLineOnAttributes = true;
            using var stream = new MemoryStream();
            var writer = XmlWriter.Create(stream, settings);
            XamlWriter.Save(button.Template, writer);
            stream.Position = 0;
            TextPanel.Text = new StreamReader(stream).ReadToEnd();
        }
    }
}
