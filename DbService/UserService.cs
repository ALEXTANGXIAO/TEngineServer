using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace TEngineServer.DbService
{
    public class UserService:BaseService
    {
        public UserService(ISqlSugarClient client) : base(client)
        {
            
        }
    }
}
