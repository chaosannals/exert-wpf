using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Di2Demo.Services;

public class AppService : BackgroundService //IHostedService, IDisposable
{
    private App app;
    private MainWindow window;
    private ILogger<AppService> logger;
    private Task? runTask;
    private CancellationTokenSource runCts;

    public AppService(App app, MainWindow mainWindow, ILogger<AppService> logger)
    {
        this.app = app;
        this.window = mainWindow;
        this.logger = logger;
        runCts = new CancellationTokenSource();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("app start in {0}", Thread.CurrentThread.ManagedThreadId);
        app.Run(window);
        //await RunAsync(stoppingToken);
        logger.LogInformation("app end in {0}", Thread.CurrentThread.ManagedThreadId);
        Environment.Exit(0);
    }

    public async Task RunAsync(CancellationToken stoppingToken)
    {
        var ts = TaskScheduler.FromCurrentSynchronizationContext();
        await Task.Run(async () =>
        {
            logger.LogInformation("app 0 run in {0}", Thread.CurrentThread.ManagedThreadId);
            await Task.Factory.StartNew(() =>
            {
                logger.LogInformation("app run in {0}", Thread.CurrentThread.ManagedThreadId);
                app.Run(window);
            }, stoppingToken, TaskCreationOptions.HideScheduler, ts)
            .ConfigureAwait(false);
        }, stoppingToken);
        //var sc = SynchronizationContext.Current;
        //var ts = TaskScheduler.FromCurrentSynchronizationContext();
        //logger.LogInformation("run in {0} {1}", Thread.CurrentThread.ManagedThreadId, ts);
        ////await Task.Delay(1000);
        //await Task.Factory.StartNew(() =>
        //{
        //    logger.LogInformation("app run in {0}", Thread.CurrentThread.ManagedThreadId);
        //    app.Run(window);
        //}, stoppingToken, TaskCreationOptions.None, ts);

        //await Task.Run(() =>
        //{
            //SynchronizationContext.SetSynchronizationContext(sc);
            //var ts = TaskScheduler.FromCurrentSynchronizationContext();
            //sc?.Send(a =>
            //{
            //    logger.LogInformation("app run in {0}", Thread.CurrentThread.ManagedThreadId);
            //    app.Run(window);
            //}, app);
        //}, stoppingToken);
    }

    //public void Dispose()
    //{
    //    runCts.Cancel();
    //}

    //public Task StartAsync(CancellationToken cancellationToken)
    //{
    //    runTask = RunAsync(runCts.Token);
    //}

    //public async Task StopAsync(CancellationToken cancellationToken)
    //{
    //    if (runTask is null)
    //    {
    //        return;
    //    }

    //    try
    //    {
    //        runCts.Cancel();
    //    }
    //    finally
    //    {
    //        await Task.WhenAny(
    //            runTask,
    //            Task.Delay(Timeout.Infinite, cancellationToken)
    //        );
    //        logger.LogInformation("udp client stop.");
    //    }
    //}
}
