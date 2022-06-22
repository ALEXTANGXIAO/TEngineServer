using DotNetty.Transport.Channels;
using TEngineProto;

namespace TEngine
{
    public class ClientMgr:Singleton<ClientMgr>
    {
        public static int SendProtoPackByClient = StringId.StringToHash("ClientMgr.SendProtoPackByClient");

        public static int SendProtoPackByIChannel = StringId.StringToHash("ClientMgr.SendProtoPackByIChannel");

        List<Client> clientList = new List<Client>();

        Dictionary<IChannel,Client> clientDic = new Dictionary<IChannel,Client>();

        public int ClientCount
        {
            get
            {
                return clientList.Count;
            }
        }

        public class BuffConfig
        {
            public int BuffID; // BuffID
            public int BuffType; // BuffType
            public float BuffValue; // Buff数值
            public float BuffValue2; // Buff数值2
            public string BuffName; // Buff名称
            public int HandleState; // 生效阶段
            public int CanAddBuff; // 可否叠加
        }

        public ClientMgr()
        {
            GameEventMgr.Instance.AddEventListener<Client,MainPack>(SendProtoPackByClient, SendProtoPack);
            GameEventMgr.Instance.AddEventListener<IChannel, MainPack>(SendProtoPackByIChannel, SendProtoPack);
        }

        private void SendProtoPack(Client client,MainPack mainPack)
        {
            client.WriteAndFlushAsync(mainPack);
        }

        private void SendProtoPack(IChannel channel, MainPack mainPack)
        {
            channel.WriteAndFlushAsync(mainPack);
        }

        public Client? GetClient(IChannel channel)
        {
            if (clientDic.ContainsKey(channel))
            {
                return clientDic[channel];
            }
            return null;
        }

        public Client? AllocClient(IChannel channel)
        {
            var client = GameMemPool<Client>.Alloc();

            client.channel = channel;

            if (!clientDic.ContainsKey(channel))
            {
                clientDic.Add(channel, client);

                clientList.Add(client);
            }
            else
            {
                return null;
            }
            return client;
        }

        public void DestroyClient(Client client)
        {
            if (clientDic.ContainsValue(client))
            {
                clientDic.Remove(client.channel);

                clientList.Remove(client);

                GameMemPool<Client>.Free(client);
            }
        }

        public void DestroyClient(IChannel channel)
        {
            if (clientDic.ContainsKey(channel))
            {
                var client = clientDic[channel];

                clientList.Remove(clientDic[channel]);

                clientDic.Remove(channel);

                GameMemPool<Client>.Free(client);
            }
        }
    }
}
