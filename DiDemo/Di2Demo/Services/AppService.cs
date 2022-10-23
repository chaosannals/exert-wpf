using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Di2Demo.Services;

public class AppService : BackgroundService
{
    private App app;
    private ILogger<AppService> logger;
    private CancellationTokenSource runCts;

    public AppService(App app, MainWindow mainWindow, ILogger<AppService> logger)
    {
        this.app = app;
        app.ShutdownMode = ShutdownMode.OnMainWindowClose;
        app.MainWindow = mainWindow;
        this.logger = logger;
        runCts = new CancellationTokenSource();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("app start in {0}", Thread.CurrentThread.ManagedThreadId);
        app.MainWindow.Show();
        await Task.Yield();
        logger.LogInformation("app end in {0}", Thread.CurrentThread.ManagedThreadId);
    }
}
