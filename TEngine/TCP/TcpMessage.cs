using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using TEngineProto;

namespace TEngine
{
    /// <summary>
    /// 服务器和客户端通信的消息格式
    /// </summary>
    public class TcpMessage
    {
        public MainPack mainPack;

        public TcpMessage()
        {

        }

        public TcpMessage(MainPack pack)
        {
            mainPack = pack;
        }
    }
}
