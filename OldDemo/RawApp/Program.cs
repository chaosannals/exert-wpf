using System.Reflection;
using System.Windows;
using System.IO;

namespace RawApp;

class Program
{
    [STAThread]
    public static void Main()
    {
        Application app = new Application();

        MainWindow main = new MainWindow();
        main.Show();

        // 更新窗口
        //var ad = AppDomain.CreateDomain("demo ad name", null);
        string ap = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RawApp.Updater.dll");
        //Assembly assembly = Assembly.Load(File.ReadAllBytes("RawApp.Updater.dll"));
        //Type? t = assembly.GetType("RawApp.Updater.UpdaterWindow");
        //if (t != null)
        //{
        //    object? i = assembly.CreateInstance("RawApp.Updater.UpdaterWindow");
        //    MethodInfo? method = t.GetMethod("Show");
        //    if (method != null && i != null)
        //    {
        //        method.Invoke(i, null);
        //        main.UcButton.Click += (send, e) =>
        //        {
        //            Window? uw = i as Window;
        //            uw?.Close();
        //        };
        //    }
        //}
        string ap2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RawApp.Updater2.dll");
        LoadAssembly(ap, "RawApp.Updater.UpdaterWindow");
        LoadAssembly(ap2, "RawApp.Updater.UpdaterWindow");


        // 主窗口

        app.Run();
    }

    public static Assembly LoadAssembly(string ap, string winname)
    {
        //Assembly assembly = Assembly.Load(File.ReadAllBytes(ap));
        Assembly assembly = Assembly.LoadFile(ap);
        Type? t = assembly.GetType(winname);
        if (t != null)
        {
            object? i = assembly.CreateInstance(winname);
            MethodInfo? method = t.GetMethod("Show");
            if (method != null && i != null)
            {
                method.Invoke(i, null);
            }
        }
        return assembly;
    }
}