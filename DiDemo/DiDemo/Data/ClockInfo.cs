using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiDemo.Data;

public class ClockInfo
{
    public DateTime Date { get; set; }
    public string Address { get; set; }
}

public class ClockList : ObservableCollection<ClockInfo>
{
    public ClockList()
    {
        Add(new ClockInfo
        {
            Date = DateTime.Now,
            Address = "地址1",
        });
        Add(new ClockInfo
        {
            Date = DateTime.Now.AddHours(2),
            Address = "地址2",
        });
        Add(new ClockInfo
        {
            Date = DateTime.Now.AddHours(4),
            Address = "地址3",
        });
    }
}