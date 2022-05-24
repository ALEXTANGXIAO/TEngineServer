using TEngineServer;
using TEngineServer.DbService;

namespace TEngine
{
    partial class SqlSugarMgr
    {
        public UserService userService;
        private List<BaseService> services = new List<BaseService>();
        public void RegisterService()
        {
            userService = new UserService(database);

            ImpRegister(userService);
        }

        private void ImpRegister(BaseService service)
        {
            services.Add(service);
        }
    }
}
