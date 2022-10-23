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

// UI 线程自带了，不需要设置。
//var sc = new DispatcherSynchronizationContext();
//SynchronizationContext.SetSynchronizationContext(sc);

var host = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(cd =>
    {
        cd.SetBasePath(Directory.GetCurrentDirectory());
        cd.AddJsonFile("appsettings.Development.json", optional: true);
        cd.AddEnvironmentVariables(prefix: "DI_DEMO2_");
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

// 因为 Run 调用 RunAsync， RunAsync（调用 StartAsync 后阻塞） 又会阻塞，导致 host 占用 UI 主线程
// 所以不能用 Run 或 RunAsync 。而应该用 StartAsync。
//await host.RunAsync();
await host.StartAsync();
host.Services.GetRequiredService<App>().Run();
await host.StopAsync();
