using System.Linq.Expressions;
using DBModel.Models;
using SqlSugar;

namespace TEngineServer
{
    public class BaseService : IBaseService
    {
        protected ISqlSugarClient SqlSugarClient;

        public BaseService(ISqlSugarClient client)
        {
            SqlSugarClient = client;
        }

        public bool Add<T>(T t) where T : class, new()
        {
            return SqlSugarClient.Insertable<T>(t).ExecuteCommand() > 0;
        }

        public bool Delete<T>(T t) where T : class, new()
        {
            return SqlSugarClient.Deleteable<T>(t).ExecuteCommand() > 0;
        }

        public bool DeleteByIndex<T>(int index) where T : class, new()
        {
            return SqlSugarClient.Deleteable<T>(index).ExecuteCommand() > 0;
        }

        public List<T> Query<T>() where T : class, new()
        {
            return SqlSugarClient.Queryable<T>().ToList();
        }

        public bool Update<T>(T t) where T : class, new()
        {
            return SqlSugarClient.Updateable<T>(t).ExecuteCommand() > 0;
        }

        public T QueryByIndex<T>(int index) where T : class, new()
        {
            return SqlSugarClient.Queryable<T>().InSingle(index);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="total"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public List<T> QueryToPage<T>(int start,int end,ref int total,Expression<Func<T,bool>> expression) where T : class, new()
        {
            return SqlSugarClient.Queryable<T>().Where(expression).ToPageList(start, end, ref total);
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public List<T> QueryCustom<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            return SqlSugarClient.Queryable<T>().Where(expression).ToList();
        }


        private void Test()
        {
            var getByWhere = SqlSugarMgr.DataBase.Queryable<User>().Where(it => it.Id == 1).ToList();//根据条件查询

        }

        //更多插入用法 http://www.codeisbug.com/Doc/8/1130
        //var total = 0;
        //var getPage = database.Queryable<Student>().Where(it => it.Id == 1).ToPageList(1, 2, ref total);
        //根据分页查询
        //多表查询用法 http://www.codeisbug.com/Doc/8/1124
    }
}
