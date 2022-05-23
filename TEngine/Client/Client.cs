using DotNetty.Transport.Channels;
using TEngineProto;

namespace TEngine
{
    public class Client : IMemPoolObject
    {
        public IChannel channel;

        public void Destroy()
        {
            channel = null;
            TLogger.LogInfo("client Destroy" + channel);
        }

        public void Init()
        {
            TLogger.LogInfo("client Init" + channel);
        }

        public void WriteAndFlushAsync(MainPack mainPack)
        {
            if (channel == null)
            {
                return;
            }
            channel.WriteAndFlushAsync(mainPack);
        }

        private void Test()
        {
            this.WriteAndFlushAsync(new TEngineProto.MainPack
            {
                Actioncode = TEngineProto.ActionCode.Login,
                Returncode = TEngineProto.ReturnCode.Success,
                Requestcode = TEngineProto.RequestCode.User
            });
        }
    }
}
