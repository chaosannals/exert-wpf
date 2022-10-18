using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Di2Demo.Services;

public class TcpService : BackgroundService
{
    private ILogger<TcpService> logger;
    private ConcurrentBag<Socket> clients;

    public IPEndPoint EndPoint { get; init; }

    public TcpService(IConfiguration configuration, ILogger<TcpService> logger)
    {
        var host = configuration.GetValue<string>("Tcp:Host");
        var port = configuration.GetValue<int>("Tcp:Port");
        this.logger = logger;
        clients = new ConcurrentBag<Socket>();
        EndPoint = new IPEndPoint(IPAddress.Parse(host), port);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.NoDelay = true;
        socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        socket.Bind(EndPoint);
        socket.Listen(0);

        logger.LogInformation("tcp server in {0} listen: {1}", Thread.CurrentThread.ManagedThreadId, EndPoint);


        // 到线程池去。
        await Task.Run(async () =>
        {
            // 该方法因为 List 子项是先获取的 Task 导致会在 当前调度器 执行。
            // 如果当前调度器是 UI 主线程的调度器，就会导致无法调度到其他线程。因为 UI 的调度器是单线程的。
            await Parallel.ForEachAsync(new List<Task>
                {
                    AcceptAsync(socket, stoppingToken),
                    DispatchAsync(stoppingToken),
                },
                stoppingToken,
                async (t, c) => await t
            );
        });
    }

    public async Task AcceptAsync(Socket server, CancellationToken stoppingToken)
    {
        logger.LogInformation("accept start in: {0}", Thread.CurrentThread.ManagedThreadId);
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var socket = await server.AcceptAsync(stoppingToken);
                clients.Add(socket);
            }
            catch (Exception e)
            {
                logger.LogError("accept error: {0}", e);
            }
        }
        logger.LogInformation("accept end in: {0}", Thread.CurrentThread.ManagedThreadId);
    }

    public async Task DispatchAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("dispatch start in: {0}", Thread.CurrentThread.ManagedThreadId);
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var sockets = clients.ToList();
                if (sockets.Count > 0)
                {
                    Socket.Select(sockets, null, null, 1000);
                    await Parallel.ForEachAsync(sockets, stoppingToken, HandleAsync);
                }
                else
                {
                    await Task.Yield();
                }
            }
            catch (Exception e)
            {
                logger.LogError("dispatch error: {0}", e);
            }
        }
        logger.LogInformation("dispatch end in: {0}", Thread.CurrentThread.ManagedThreadId);
    }

    public async ValueTask HandleAsync(Socket socket, CancellationToken cancellationToken)
    {
        try
        {
            await Task.Yield();
        }
        catch (Exception e)
        {
            socket.Close();
            logger.LogError("handle error: {0}", e);
        }
    }
}
