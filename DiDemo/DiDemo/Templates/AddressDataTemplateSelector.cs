using DiDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DiDemo.Templates;

public class AddressDataTemplateSelector : DataTemplateSelector
{
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is AddressInfo)
        {
            var addressInfo = item as AddressInfo;
            var fe = container as FrameworkElement;
            if (addressInfo.StreetName == "街道1")
            {
                return fe!.FindResource("dt2") as DataTemplate;
            }
            return fe!.FindResource("dt1") as DataTemplate;
        }
        return null;
    }
}
