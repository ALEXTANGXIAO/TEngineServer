using DotNetty.Transport.Channels;
using SocketGameProtocol;

namespace TEngine
{
    class UserController : BaseController
    {
        public UserController()
        {
            requestCode = RequestCode.User;
        }

        public MainPack Register(IChannelHandlerContext channel, MainPack mainPack)
        {
            //TODO
            mainPack.Returncode = ReturnCode.Fail;
            return mainPack;
        }

        public MainPack Login(IChannelHandlerContext channel, MainPack mainPack)
        {
            //TODO
            mainPack.Returncode = ReturnCode.Fail;
            return mainPack;
        }
    }
}
