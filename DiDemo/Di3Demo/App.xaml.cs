using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Di3Demo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public ServiceCollection Services { get; init; }
    public ServiceProvider Provider { get; init; }
    
    public App()
    {
        Services = new ServiceCollection();
        Services.AddSingleton(_ => this);
        Services.AddSingleton<MainWindow>();
        Provider = Services.BuildServiceProvider();
        MainWindow = Provider.GetRequiredService<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow.Show();
        base.OnStartup(e);
    }
}
