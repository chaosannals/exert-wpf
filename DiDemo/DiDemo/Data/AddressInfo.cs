using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiDemo.Data;

public class AddressInfo
{
    public string StreetName { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}

public class AddressList : ObservableCollection<AddressInfo>
{
    public AddressList(): base()
    {
        Add(new AddressInfo
        {
            Country = "中国",
            City = "汕头",
            StreetName = "街道1",
        });
        Add(new AddressInfo
        {
            Country = "中国",
            City = "汕头",
            StreetName = "街道2",
        });
        Add(new AddressInfo
        {
            Country = "中国",
            City = "汕头",
            StreetName = "街道3",
        });
        Add(new AddressInfo
        {
            Country = "中国",
            City = "汕头",
            StreetName = "街道4",
        });
        Add(new AddressInfo
        {
            Country = "中国",
            City = "汕头",
            StreetName = "街道5",
        });
    }
}