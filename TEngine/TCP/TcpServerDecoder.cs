using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Google.Protobuf;
using SocketGameProtocol;

namespace TEngine
{
    /// <summary>
    /// TCPServer解码器
    /// </summary>
    public class TcpServerDecoder : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            var mainPack = ProtoUtil.Deserialize(input.Array);
            TLogger.LogInfo(context.Channel.RemoteAddress +":Received from client:" + mainPack);
            output.Add(mainPack);
            input.Clear();
        }
    }
}
