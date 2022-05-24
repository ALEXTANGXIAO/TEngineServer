using System.Linq.Expressions;

namespace TEngineServer
{
    public interface IBaseService
    {
        public bool Add<T>(T t) where T : class ,new();

        public bool Delete<T>(T t) where T : class, new();

        public bool DeleteByIndex<T>(int index) where T : class, new();

        public List<T> Query<T>() where T : class, new();

        public T QueryByIndex<T>(int index) where T : class, new();

        public List<T> QueryToPage<T>(int start, int end, ref int total, Expression<Func<T, bool>> expression)
            where T : class, new();

        public List<T> QueryCustom<T>(Expression<Func<T, bool>> expression) where T : class, new();

        public bool Update<T>(T t) where T : class, new();
    }
}
