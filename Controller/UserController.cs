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
            var userName = mainPack.LoginPack.Username;
            var result = SqlSugarMgr.DataBase.Queryable<User>().Where(user => user.UserName == userName).ToList();
            if (result.Count > 0)
            {
                mainPack.Returncode = ReturnCode.Fail;
                mainPack.Extstr = "账号已经存在";
            }

            var user = new User()
            {
                UserName = userName,
                Password = mainPack.LoginPack.Password,
                RoleName = "",
            };
            userService.Add(user);

            mainPack.Returncode = ReturnCode.Success;
            mainPack.Extstr = "注册成功";
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
                if (mainPack.LoginPack.Password == result[0].Password)
                {
                    mainPack.Returncode = ReturnCode.Success;
                    mainPack.Extstr = "登陆成功";
                }
                else
                {
                    mainPack.Returncode = ReturnCode.Fail;
                    mainPack.Extstr = "密码错误";
                }
            }
            
            return mainPack;
        }
    }
}
