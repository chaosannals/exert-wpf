using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace HaloKid
{
    public class HkV : DependencyObject
    {
        public static DependencyProperty TargetPage = DependencyProperty.RegisterAttached("TargetPage", typeof(string), typeof(HkV));
        public static DependencyProperty TargetPanel = DependencyProperty.RegisterAttached("TargetPanel", typeof(string), typeof(HkV));

        public static string GetTargetPage(DependencyObject owner)
        {
            return owner.GetValue(TargetPage) as string;
        }

        public static void SetTargetPage(DependencyObject owner, string name)
        {
            owner.SetValue(TargetPage, name);
        }

        public static string GetTargetPanel(DependencyObject owner)
        {
            return owner.GetValue(TargetPanel) as string;
        }

        public static void SetTargetPanel(DependencyObject owner, string name)
        {
            owner.SetValue(TargetPanel, name);
        }
    }
}
