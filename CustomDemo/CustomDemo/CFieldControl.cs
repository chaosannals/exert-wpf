using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomDemo;

public class CFieldControl : Control
{
    public static readonly DependencyProperty MyBoolProperty = DependencyProperty.Register(
      name: "MyBool",
      propertyType: typeof(bool),
      ownerType: typeof(CFieldControl),
      typeMetadata: new FrameworkPropertyMetadata(
          defaultValue: false,
          flags: FrameworkPropertyMetadataOptions.AffectsRender,
          propertyChangedCallback: new PropertyChangedCallback((d, e) =>
          {

          }))
    );

    public bool MyBool
    {
        get => (bool)GetValue(MyBoolProperty);
        set => SetValue(MyBoolProperty, value);
    }
}
