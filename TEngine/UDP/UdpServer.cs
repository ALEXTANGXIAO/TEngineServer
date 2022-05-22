using DotNetty.Buffers;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System.Net;

namespace TEngine.UDP
{
    public static class UdpServer
    {
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
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, port);

            eventLoop = new MultithreadEventLoopGroup();
            try
            {
                var bootstrap = new Bootstrap();
                //设置线程组模型为：单线程模型
                bootstrap.Group(eventLoop);
                //设置通道类型UDP连接方式
                bootstrap.Channel<SocketDatagramChannel>(); 
                bootstrap
                    //广播形式获取数据
                    .Option(ChannelOption.SoBroadcast, true)
                    //可以复用端口号
                    .Option(ChannelOption.SoReuseaddr, true)
                    //设置重用缓冲区
                    .Option(ChannelOption.Allocator, PooledByteBufferAllocator.Default)
                    //用于对单个通道的数据处理
                    .Handler(new ActionChannelInitializer<IChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        pipeline.AddLast(new LoggingHandler());
                        pipeline.AddLast(new UdpHandler());
                    }));
                    bootstrapChannel = await bootstrap.BindAsync(clientEndPoint); //绑定本地端口号开始监听

                    if (!bootstrapChannel.Active)
                    {
                        await bootstrapChannel.CloseAsync();
                    }

                TLogger.LogInfoSuccessd($"启动UDP服务器成功，端口号:{port}");

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
