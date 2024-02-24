using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalendarDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int times {  get; set; }
        private DateTime? startAt { get; set; }

        private bool selfAction { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();
            //dateRangePicker.DisplayDateStart = DateTime.Now;
            //dateRangePicker.DisplayDateEnd = DateTime.Now.AddDays(200);
            //var range = new CalendarDateRange(DateTime.Today, DateTime.Today.AddDays(200));
            dateRangePicker.SelectedDates.AddRange(DateTime.Today, DateTime.Today.AddDays(200));
            dateRangePicker.SelectedDatesChanged += (s, e) =>
            {
                if (selfAction) { selfAction = false; return; }
                times++;
                Debug.WriteLine($"times: {times}");
                if (times % 2 == 1)
                {
                    Debug.WriteLine($"start add: {e.AddedItems[0]?.GetType().Name}");
                    startAt = (e.AddedItems[0] as DateTime?);
                    selfAction = true;
                    dateRangePicker.SelectedDates.Clear();
                }
                else
                {
                    Debug.WriteLine($"end");
                    var endAt = (e.AddedItems[0] as DateTime?);
                    selfAction = true;
                    dateRangePicker.SelectedDates.Clear();
                    selfAction = true;
                    dateRangePicker.SelectedDates.AddRange(startAt!.Value, endAt!.Value);
                }
            };
        }
    }
}