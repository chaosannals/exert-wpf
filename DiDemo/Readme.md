# wfp 依赖注入

## DiDemo

比较推荐，这种方式下，App.Run 先执行，没有明显缺陷，只要注意 HostedService 不要占用太多 UI 线程时间就不会有问题。

## Di2Demo

Di2Demo 有一些缺点，App.xaml 生成的代码被无视了，不是通过 App.Main 执行的。
而且 App 构造函数不好直接使用依赖注入，需要手写注入方式，因为 App.xaml 需要一个无参构造函数（编译了，但是没有被用到）。

由于 App.Run 比 host.StartAsync 慢，导致了多个 AddHostedService 的异步调用使用线程池的话在 host.StopAsync 的时候会占用，
必须使用 TaskCreationOptions.LongRunning 使得独立线程来避免这种情况。

## Di3Demo

这种只使用最简单的依赖注入，没有 Host 也就没有 StartAsync 和 StopAsync 和 App.Run 竞抢主从。
缺点是没有了 Host 也就失去了很多便捷的插件，日志等都要自己去处理。
毕竟 Host 提供了 Asp.Net Core 的兼容性，使得 Asp.Net Core 的组件可以直接用上，便捷了不少。
同时没有 Host 也失去了如 后台服务这些功能都要自己实现（StartAsync 和 StopAsync 就是在启动这些服务）。
