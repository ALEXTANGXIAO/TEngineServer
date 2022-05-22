using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using SocketGameProtocol;
using System;
using System.Text;

namespace TEngine
{
    public class TcpServerHandler : SimpleChannelInboundHandler<MainPack>
    {
        /// <summary>
        /// TCP处理客户端通信（DeCode解码后执行）
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="msg"></param>
        protected override void ChannelRead0(IChannelHandlerContext context, MainPack mainPack)
        {
            if(mainPack != null)
            {
                GameEventMgr.Instance.Send(ControllerManager.HandleProtoPack, context, mainPack);

                //context.WriteAndFlushAsync(mainPack);
   
                TLogger.LogInfoSuccessd("TcpServe Send"+ mainPack);
            }
        }

        /// <summary>
        /// TCP链路建立成功
        /// </summary>
        /// <param name="context"></param>
        public override void ChannelActive(IChannelHandlerContext context)
        {
            TLogger.LogInfo($"{context.Channel.RemoteAddress}:建立连接 listClients Count:{ClientMgr.Instance.ClientCount}");
            base.ChannelActive(context);
        }

        /// <summary>
        /// TCP链路丢失
        /// </summary>
        /// <param name="context"></param>
        public override void ChannelInactive(IChannelHandlerContext context)
        {
            ClientMgr.Instance.DestroyClient(context.Channel);
            TLogger.LogInfo($"{context.Channel.RemoteAddress}:断开链接 listClients Count:{ClientMgr.Instance.ClientCount}");
            base.ChannelInactive(context);
        }

        /// <summary>
        /// 链路异常
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            TLogger.LogException(exception.ToString());
            base.ExceptionCaught(context, exception);
        }
    }
}
