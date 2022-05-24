using DBModel.Models;
using DotNetty.Transport.Channels;
using TEngineProto;
using TEngineServer.DbService;

namespace TEngine
{
    class UserController : BaseController
    {
        private UserService userService = SqlSugarMgr.Instance.userService;

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
            var userName = mainPack.LoginPack.Username;
            var result = SqlSugarMgr.DataBase.Queryable<User>().Where(user => user.UserName == userName).ToList();
            if (result.Count <= 0)
            {
                mainPack.Returncode = ReturnCode.Fail;
                mainPack.Extstr = "没有该账号";
            }
            else
            {

            }
            
            return mainPack;
        }
    }
}
