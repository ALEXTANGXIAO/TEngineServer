using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Google.Protobuf;
using TEngineProto;

namespace TEngine
{
    /// <summary>
    /// TCPServer解码器
    /// </summary>
    public class TcpServerDecoder : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            var length = 4 + input.Array[0];
            IByteBuffer result = Unpooled.Buffer();
            try
            {
                input.ReadBytes(result, length);
                var mainPack = ProtoUtil.Deserialize(result.Array);
                TLogger.LogInfo(context.Channel.RemoteAddress + ":Received from client:" + mainPack);
                output.Add(mainPack);
            }
            catch (Exception e)
            {
                ClientMgr.Instance.DestroyClient(context.Channel);
                TLogger.LogException(e.ToString());
            }
            result.Clear();
            input.Clear();
        }
    }
}
