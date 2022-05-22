using System.Reflection;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Google.Protobuf;
using SocketGameProtocol;

namespace TEngine
{
    public class ControllerManager : TSingleton<ControllerManager>
    {
        public static int HandleProtoPack = StringId.StringToHash("ControllerManager.HandleMainPack");

        private Dictionary<RequestCode, BaseController> controllerDic = new Dictionary<RequestCode, BaseController>();

        object[] cache = new object[2] { null, null };

        public void RegisterController(RequestCode requestCode, BaseController controller)
        {
            if (controller == null)
            {
                TLogger.LogError($"{controller} CouldNot Be None!");

                return;
            }
            if (!controllerDic.ContainsKey(requestCode))
            {
                TLogger.LogInfoSuccessd($"{requestCode} Controller Register Successed");

                controllerDic[requestCode] = controller;
            }
            else
            {
                TLogger.LogError($"{requestCode} Had Registered!");
            }
        }

        protected override void Init()
        {
            GameEventMgr.Instance.AddEventListener<IChannelHandlerContext, MainPack>(HandleProtoPack, HandleMainPack);
        }

        public override void Active()
        {
            
        }

        public override void Release()
        {
            controllerDic.Clear();
            base.Release();
        }

        private void HandleMainPack(IChannelHandlerContext channel, MainPack mainPack)
        {
            if (controllerDic.TryGetValue(mainPack.Requestcode, out BaseController controller))
            {
                string methodname = mainPack.Actioncode.ToString();                 
                MethodInfo? method = controller.GetType().GetMethod(methodname);
                if (method == null)
                {
                    TLogger.LogError($"{mainPack.Requestcode} Had None This Method:{mainPack.Actioncode}");
                    return;
                }

                cache[0] = channel;
                cache[1] = mainPack;

                object? ret = method.Invoke(controller, cache);
                if (ret != null)
                {
                    var pack = ret as MainPack;

                    if (pack != null)
                    {
                        channel.WriteAndFlushAsync(pack);
                    }
                }

                cache[0] = null;
                cache[1] = null;
            }
            else
            {
                TLogger.LogError($"{mainPack.Requestcode} Did Not Register!");
            }
        }
    }
}
