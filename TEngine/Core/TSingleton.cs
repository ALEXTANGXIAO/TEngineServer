using System.Collections.Generic;

namespace TEngine
{
    /// <summary>
    /// 单例接口
    /// </summary>
    public interface ISingleton
    {
        void Active();

        void Release();
    }

    /// <summary>
    /// 单例管理器(统一化持久和释放)
    /// </summary>
    public static class SingletonMgr
    {
        private static List<ISingleton> _iSingletonList;
        public static void Retain(ISingleton go)
        {
            if (_iSingletonList == null)
            {
                _iSingletonList = new List<ISingleton>();
            }
            _iSingletonList.Add(go);
        }

        public static void Release(ISingleton go)
        {
            if (_iSingletonList != null && _iSingletonList.Contains(go))
            {
                _iSingletonList.Remove(go);
            }
        }

        public static void Release()
        {
            if (_iSingletonList != null)
            {
                for (int i = 0; i < _iSingletonList.Count; ++i)
                {
                    _iSingletonList[i].Release();
                }

                _iSingletonList.Clear();
            }
        }

        internal static ISingleton? GetSingleton(string name)
        {
            for (int i = 0; i < _iSingletonList.Count; ++i)
            {
                if (_iSingletonList[i].ToString() == name)
                {
                    return _iSingletonList[i];
                }
            }
            return null;
        }
    }

    /// <summary>
    /// 全局单例对象（非线程安全）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TSingleton<T> : ISingleton where T : TSingleton<T>, new()
    {
        protected static T _instance = default(T);

        public static T Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new T();
                    _instance.Init();
#if UNITY_EDITOR
                    TLogger.LogInfo($"TSingleton Instance:{typeof(T).Name}");
#endif
                    SingletonMgr.Retain(_instance);
                }
                return _instance;
            }
        }

        public static bool IsValid
        {
            get
            {
                return _instance != null;
            }
        }

        protected TSingleton()
        {

        }

        protected virtual void Init()
        {

        }

        public virtual void Active()
        {

        }

        public virtual void Release()
        {
            if (_instance != null)
            {
                SingletonMgr.Release(_instance);
                _instance = null;
            }
        }
    }
}