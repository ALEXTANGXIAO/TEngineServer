using DotNetty.Buffers;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

namespace TEngine
{
    /// <summary>
    /// TEngine TcpServer
    /// </summary>
    public static class TcpServer
    {
        public static Int64 count = 0;
        public static DateTime dt1 = DateTime.Now;
        public static DateTime dt2 = DateTime.Now.AddSeconds(1);

        static IChannel? bootstrapChannel;

        /// <summary>
        /// 主线程
        /// </summary>
        static IEventLoopGroup? eventLoop;

        /// <summary>
        /// 工作线程
        /// </summary>
        static IEventLoopGroup? workerLoop;
        /// <summary>
        /// 启动TcpServer
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static Task Start(int port)
        {
            return RunServerAsync(port);
        }

        private static async Task RunServerAsync(int port)
        {
            //InternalLoggerFactory.DefaultFactory.AddProvider(provider);
            //主工作组
            eventLoop = new MultithreadEventLoopGroup(1);
            //子工作组 默认是 CPU核心数目 * 2
            workerLoop = new MultithreadEventLoopGroup();
            try
            {
                var bootstrap = new ServerBootstrap();
                //设置线程组模型为：主从线程模型
                bootstrap.Group(eventLoop, workerLoop);
                //设置通道类型
                bootstrap.Channel<TcpServerSocketChannel>();

                bootstrap
                    //半连接队列的元素上限 
                     .Option(ChannelOption.SoBacklog, 4096)
                     //设置Channel接的字节流时的缓冲区大小
                     .Option(ChannelOption.RcvbufAllocator, new AdaptiveRecvByteBufAllocator())
                     //设置重用缓冲区
                     .Option(ChannelOption.Allocator, UnpooledByteBufferAllocator.Default)
                    //.Option(ChannelOption.Allocator, PooledByteBufferAllocator.Default)
                    .ChildOption(ChannelOption.Allocator, UnpooledByteBufferAllocator.Default)
                    //保持长连接
                    .ChildOption(ChannelOption.SoKeepalive, true)
                    //取消延迟发送（关闭Nagle算法）
                    .ChildOption(ChannelOption.TcpNodelay, true)

                    .Handler(new LoggingHandler(LogLevel.INFO))
                    //用于对单个通道的数据处理
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;

                        pipeline.AddLast(new LoggingHandler(LogLevel.INFO));

                        //pipeline.AddLast(new EchoServerHandler());

                        pipeline.AddLast(new TcpServerEncoder(), new TcpServerDecoder(), new TcpServerHandler());

                        ClientMgr.Instance.AllocClient(channel);

                        TLogger.LogInfoSuccessd($"listClients Count:{ClientMgr.Instance.ClientCount}");
                    }));
                bootstrapChannel = await bootstrap.BindAsync(port);

                TLogger.LogInfoSuccessd($"启动TCP服务器成功，端口号:{port}");

                Console.ReadLine();

                Stop().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                throw new Exception("启动TCP服务器 失败\n" + ex.StackTrace);
            }
            finally
            {
                await workerLoop.ShutdownGracefullyAsync();

                await eventLoop.ShutdownGracefullyAsync();
            }
        }

        public static async Task Stop()
        {
            await Task.WhenAll(
                bootstrapChannel.CloseAsync(),
                eventLoop.ShutdownGracefullyAsync(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2)),
                workerLoop.ShutdownGracefullyAsync(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2))
                );
        }
    }
}
