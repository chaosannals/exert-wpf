using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;
using Serilog;
using Serilog.Events;

namespace CSClientDemo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; } = null!;
    public IConfiguration Configuration { get; private set; } = null!;

    public SystemTray Tray { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        Tray = new SystemTray();
        Tray.ClickLeftButton += (o, e) =>
        {
            Shutdown();
        };

        Configuration = builder.Build();

        ShutdownMode = ShutdownMode.OnExplicitShutdown;

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<ILogger>(op =>
         {
             var path = Configuration.GetRequiredSection("Logger:PathFormat");
             return new LoggerConfiguration()
                 .MinimumLevel.Information()
                 .WriteTo.File(
                         path: path?.ToString() ?? "Logs/S-{Date}.log",
                         rollingInterval: RollingInterval.Day,
                         rollOnFileSizeLimit: true,
                         fileSizeLimitBytes: int.Parse(Configuration.GetSection("Logger:FileSizeLimitBytes")?.Value ?? "2000000"),
                         retainedFileCountLimit: int.Parse(Configuration.GetSection("Logger:RetainedFileCountLimit")?.Value ?? "31"),
                         flushToDiskInterval: TimeSpan.FromSeconds(10),
                         outputTemplate: Configuration.GetSection("Logger:OutputTemplate")?.Value ?? "[{Timestamp:yy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                     )
                     .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                     .CreateLogger();
         });

        // serviceCollection.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
        serviceCollection.AddTransient(typeof(MainWindow));
        //ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        //mainWindow.Show();
    }

}
