# TEngineServer

<p align="center">
    <img src="http://1.12.241.46:8081/temp/TEngine512.png" alt="logo" width="384" height="384">
</p>

<h3 align="center"><strong>TEngineServer<strong></h3>

<p align="center">
  <strong>Unity框架解决方案 服务端TEngineServer<strong>
    <br>
  <a style="text-decoration:none">
    <img src="https://img.shields.io/badge/Unity%20Ver-2019.4.12++-blue.svg?style=flat-square" alt="status" />
  </a>
  <a style="text-decoration:none">
    <img src="https://img.shields.io/github/license/ALEXTANGXIAO/TEngine" alt="license" />
  </a>
  <a style="text-decoration:none">
    <img src="https://img.shields.io/github/last-commit/ALEXTANGXIAO/TEngine" alt="last" />
  </a>
  <a style="text-decoration:none">
    <img src="https://img.shields.io/github/issues/ALEXTANGXIAO/TEngine" alt="issue" />
  </a>
  <a style="text-decoration:none">
    <img src="https://img.shields.io/github/languages/top/ALEXTANGXIAO/TEngine" alt="topLanguage" />
  </a>
  <a style="text-decoration:none">
    <img src="https://app.fossa.com/api/projects/git%2Bgithub.com%2FJasonXuDeveloper%2FJEngine.svg?type=shield" alt="status" />
  </a>
  <br>
  
  <br>
</p>



# <strong>TEngineServer v1.0.0
## <a href="http://1.12.241.46:5000/"><strong>文档快速入门 »</strong></a>
## <a href="https://github.com/ALEXTANGXIAO/TEngine"><strong>客户端GitHub入口 »</strong></a>

## TEngineServer是一个基于.Net Core的游戏服务端框架。
.Net Core现在已经更新到了6.0的版本，在性能和设计上其实是远超JAVA。在JAVAER还在为JVM更新和添加更多功能时，其实他们已经被国内大环境所包围了，看不到.Net Core的性能之强，组件化的结构。其实.Net Core最大的问题是大多数自己人都不知道他的优点，甚至很多守旧派抵制core。GO喜欢吹性能，但其实目前来看，除了协程的轻量级，大多数性能测试其实不如JAVA和.Net。

### 回归正题，TEngine使用了.Net Core6.0 -- DotNetty框架(异步事件驱动)
<strong>底层性能强悍开发者使用TEngineServer不用关心底层Socket的编写与转发，您只需要编写控制器和编译Protobuf协议即可快速上手！！！

```Csharp
数据流动会从 TcpServer -> TcpServerDecoder(TCP解码器解析客户端发来的消息包) -> TcpServerHandler(TCP服务器Handle) -> TEngine.GameEventMgr(TEngine事件分发) -> ControllerManager(处理请求类型的事件)-> UserController(处理事件逻辑) -> ControllerManager(Pack序列化包体给客户端) -> TcpServerEncode(TCP编码器加密序列化即将发送给客户端的消息包) -> DotNetty底层线程池的工作队列发送消息包

TEngineServer只用关注Controller的实现和内部MainPack的处理。


//案例登录
//编写控制器派生BaseController
class UserController : BaseController
    {
        public UserController()
        {
            requestCode = RequestCode.User;
        }
        //实现注册方法
        public MainPack Register(IChannelHandlerContext channel, MainPack mainPack)
        {
            //TODO
            mainPack.Returncode = ReturnCode.Fail;
            return mainPack;
        }

        //实现登录方法
        public MainPack Login(IChannelHandlerContext channel, MainPack mainPack)
        {
            //TODO
            mainPack.Returncode = ReturnCode.Fail;
            return mainPack;
        }
    }


//在合适的时候注册控制器
static void RegisterController()
{
    ControllerManager.Instance.RegisterController(SocketGameProtocol.RequestCode.User, new UserController());
}


```

---

引用类库：DotNetty

1. DotNetty.Buffers：对内存缓冲区管理的封装
2. DotNetty.Codecs：对编解码的封装，包括一些基础基类的实现
3. DotNetty.Common：公共的类库项目，包装线程池，并行任务和常用帮助类的封装
4. DotNetty.Handlers：封装了常用的管道处理器，比如tls编解码，超时机制，心跳检查，日志等
5. DotNetty.Transport：DotNetty核心的实现

DotNetty 是一个提供 asynchronous event-driven （异步事件驱动）的网络应用框架，是一个用以快速开发高性能、可扩展协议的服务器和客户端。

　　换句话说，DotNetty 是一个 NIO 客户端服务器框架，使用它可以快速简单地开发网络应用程序，比如服务器和客户端的协议。DotNetty 大大简化了网络程序的开发过程比如 TCP 和 UDP 的 socket 服务的开发。

“快速和简单”并不意味着应用程序会有难维护和性能低的问题，DotNetty 是一个精心设计的框架，它从许多协议的实现中吸收了很多的经验比如 FTP、SMTP、HTTP、许多二进制和基于文本的传统协议.因此，Netty 已经成功地找到一个方式,在不失灵活性的前提下来实现开发的简易性，高性能，稳定性。

## 快速使用
 解决方案管理器窗口/管理NuGet包/添加DotNetty

## <strong>技术支持
 QQ群：967860570   
 欢迎大家提供意见和改进意见，不喜请友善提意见哈 谢谢~
