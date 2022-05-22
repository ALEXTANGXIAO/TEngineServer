using SqlSugar;

namespace TEngine
{
    //TODO 
    public class SqlSugarMgr : TSingleton<SqlSugarMgr>
    {
        //用单例模式
        private static SqlSugarScope database = new SqlSugarScope(new ConnectionConfig()
        {
            ConnectionString = "server=127.0.0.1;port=3306;uid=root;pwd=1234567;database=TEngine",
            DbType = DbType.MySql,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute     //从实体特性中读取主键自增列信息
        },
         db =>
         {
             //(A)全局生效配置点
             //调试SQL事件，可以删掉
             db.Aop.OnLogExecuting = (sql, pars) =>
             {
                 TLogger.LogInfo(sql + "\r\n" +
                 db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                 Console.WriteLine();
             };

         });

        public SqlSugarMgr()
        {
            
        }

        #region Query
        public List<T>? QueryableList<T>()
        {
            if (database != null)
            {
                if (database.Queryable<T>() != null)
                {
                    return database.Queryable<T>().ToList();
                }
            }

            return null;
        }

        public T? QueryableById<T>(int index)
        {
            if (database != null)
            {
                if (database.Queryable<T>() != null)
                {
                    return database.Queryable<T>().InSingle(index);
                }
            }

            return default(T);
        }
        #endregion



        private void Test()
        {

            /*查询*/
            var list = database.Queryable<StudentModel>().ToList();//查询所有
            var getById = database.Queryable<StudentModel>().InSingle(1);//根据主键查询
            var getByWhere = database.Queryable<StudentModel>().Where(it => it.Id == 1).ToList();//根据条件查询
            var total = 0;
            var getPage = database.Queryable<StudentModel>().Where(it => it.Id == 1).ToPageList(1, 2, ref total);//根据分页查询
                                                                                                           //多表查询用法 http://www.codeisbug.com/Doc/8/1124

            /*插入*/
            var data = new Student() { Name = "jack" };
            database.Insertable(data).ExecuteCommand();
            //更多插入用法 http://www.codeisbug.com/Doc/8/1130

            /*更新*/
            var data2 = new Student() { Id = 1, Name = "jack" };
            database.Updateable(data2).ExecuteCommand();
            //更多更新用法 http://www.codeisbug.com/Doc/8/1129

            /*删除*/
            database.Deleteable<StudentModel>(1).ExecuteCommand();
        }

        public void CreateDatabase()
        {
            database.CodeFirst.SetStringDefaultLength(200/*设置varchar默认长度为200*/).InitTables(typeof(StudentModel));//执行完数据库就有这个表了
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    //如果实体类名称和表名不一致可以加上SugarTable特性指定表名
    [SugarTable("Student")]
    public class StudentModel
    {
        //指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
