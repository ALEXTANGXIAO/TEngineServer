using System.Reflection;
using DBModel.Models;
using SqlSugar;
using TEngineServer.DbService;

namespace TEngine
{
    partial class SqlSugarMgr : TSingleton<SqlSugarMgr>
    {
        #region 成员变量

        private const string ConnectionString = "server=127.0.0.0;port=3306;uid=root;pwd=123456!;database=TEngine";
        public static SqlSugarScope DataBase
        {
            get
            {
                return database;
            }
        }

        private static SqlSugarScope database = new SqlSugarScope(new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute     //从实体特性中读取主键自增列信息
            },
            db =>
            {
                bool showSql = true;
                if (showSql)
                {
                    db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        TLogger.LogInfo(sql + "\r\n" +
                                        db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                        Console.WriteLine();
                    };
                }
            });
        #endregion


        public void InitDataBase()
        {
            TLogger.LogInfoSuccessd("开始初始化/连接数据库");
            ConnectionConfig config = new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            };

            Assembly assembly = Assembly.LoadFrom("DBModel.dll");
            IEnumerable<Type> typeList = assembly.GetTypes().Where(c => c.Namespace == "DBModel.Models");

            bool backUp = false;

            using (SqlSugarClient client = new SqlSugarClient(config))
            {
                bool showSql = true;
                if (showSql)
                {
                    client.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        TLogger.LogInfo(sql + "\r\n" +
                                        client.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                        Console.WriteLine();
                    };
                }

                client.DbMaintenance.CreateDatabase();
                if (backUp)
                {
                    client.CodeFirst.BackupTable().InitTables(typeList.ToArray());
                }
                else
                {
                    client.CodeFirst.InitTables(typeList.ToArray());
                }
            }

            TLogger.LogInfoSuccessd("初始化数据库成功");

            RegisterService();
        }
    }
}
