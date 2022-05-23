using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Google.Protobuf;
using TEngineProto;

namespace TEngine
{
    /// <summary>
    /// TCPServer编码器（context.WriteAndFlushAsync(msg)后处理）
    /// </summary>
    public class TcpServerEncoder: MessageToByteEncoder<MainPack>
    {
        protected override void Encode(IChannelHandlerContext context, MainPack message, IByteBuffer output)
        {
            output.WriteBytes(ProtoUtil.Serialize(message));
        }
    }
}
