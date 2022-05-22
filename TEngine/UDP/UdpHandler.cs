using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

namespace TEngine.UDP
{
    internal class UdpHandler : SimpleChannelInboundHandler<DatagramPacket>
    {
        public override bool IsSharable => true;

        protected override void ChannelRead0(IChannelHandlerContext ctx, DatagramPacket packet)
        {
            var byteBuffer = packet.Content; //将收到的数据转换为IByteBuffer , 可通过ReadByte()方法读取单个byte类型 , 此外还有ReadInt() , ReadString()等.

            var sender = packet.Sender;
      
            TLogger.LogInfo("Receive" + sender + ":" +"收到数据:" + byteBuffer.ToString());

        }
        public override void ChannelReadComplete(IChannelHandlerContext ctx)
        {
            ctx.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception e)
        {
            TLogger.LogInfo("{0}", e.ToString());
            ctx.CloseAsync();
        }
    }
}
