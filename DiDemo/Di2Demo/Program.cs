using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading;
using Di2Demo;
using Di2Demo.Services;

if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
{
    Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
    Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
}

var sc = new SynchronizationContext();
SynchronizationContext.SetSynchronizationContext(sc);

var host = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(cd =>
    {
        cd.SetBasePath(Directory.GetCurrentDirectory());
        cd.AddJsonFile("appsettings.Development.json", optional: true);
        cd.AddEnvironmentVariables(prefix: "DI_DEMO_");
    })
    .ConfigureLogging((hc, cl) =>
    {
        cl.AddFile(hc.Configuration.GetSection("LoggingFile"));
        cl.AddConsole();
    })
    .ConfigureServices((hc, services) =>
    {
        services.AddSingleton<App>();
        services.AddSingleton<MainWindow>();
        services.AddHostedService<AppService>();
        services.AddHostedService<TcpService>();
    })
    .Build();

await host.RunAsync();
