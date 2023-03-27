using DiDemo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace DiDemo.Views;

/// <summary>
/// DataBindingPage.xaml 的交互逻辑
/// </summary>
public partial class DataBindingPage : Page
{
    public DataBindingPage()
    {
        InitializeComponent();
        var addressList = FindResource("addressList") as AddressList;
        var view = CollectionViewSource.GetDefaultView(addressList);
        view.GroupDescriptions.Clear();

        // 添加分组
        view.GroupDescriptions.Add(new PropertyGroupDescription("City"));

        // 添加排序
        view.SortDescriptions.Clear();
        view.SortDescriptions.Add(new SortDescription("StreetName", ListSortDirection.Descending));

        // 过滤
        view.Filter = new Predicate<object>((t) =>
        {
            var addr = t as AddressInfo;
            return !addr!.StreetName.Contains("3");
        });
    }

    private void OnPrev(object sender, RoutedEventArgs e)
    {
        var addressList = FindResource("addressList") as AddressList;
        var view = CollectionViewSource.GetDefaultView(addressList);
        view.MoveCurrentToPrevious();
        if (view.IsCurrentBeforeFirst)
        {
            view.MoveCurrentToLast();
        }
    }

    private void OnNext(object sender, RoutedEventArgs e)
    {
        var addressList = FindResource("addressList") as AddressList;
        var view = CollectionViewSource.GetDefaultView(addressList);
        view.MoveCurrentToNext();
        if (view.IsCurrentAfterLast)
        {
            view.MoveCurrentToFirst();
        }
    }

    private void OnAdd(object sender, RoutedEventArgs e)
    {
        var addressList = FindResource("addressList") as AddressList;
        var view = CollectionViewSource.GetDefaultView(addressList);
        addressList?.Add(new AddressInfo { Country="新国", City="新市", StreetName="新街"});
        view.MoveCurrentToLast();
    }

    private void OnDel(object sender, RoutedEventArgs e)
    {
        var addressList = FindResource("addressList") as AddressList;
        var view = CollectionViewSource.GetDefaultView(addressList);
        addressList?.Remove(view.CurrentItem as AddressInfo);
    }
}
