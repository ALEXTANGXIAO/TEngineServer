using DotNetty.Buffers;
using Google.Protobuf;
using TEngineProto;

class ProtoUtil
{
    private const int BufferHead = 4;

    public static MainPack? Deserialize(byte[] data)
    {
        if(data == null)
        {
            return null;
        }
        int count = BitConverter.ToInt32(data, 0);

        MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(data, BufferHead, count);

        return pack;
    }

    public static byte[] Serialize(MainPack pack)
    {
        byte[] data = pack.ToByteArray();

        byte[] head = BitConverter.GetBytes(data.Length);

        return head.Concat(data).ToArray();
    }

    public static byte[] SerializeUDP(MainPack pack)
    {
        return pack.ToByteArray();
    }
}