namespace TEngine
{
    /// <summary>
    /// 线程安全单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : new()
    {
        private static bool m_ShuttingDown = false;
        private static object m_Lock = new object();
        private static T _instance;

        public static T? Instance
        {
            get
            {
                if (m_ShuttingDown)
                {
                    TLogger.LogWarning("[Singleton] Instance '" + typeof(T) +"' already destroyed. Returning null.");
                    return default(T);
                }

                lock (m_Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }

                    return _instance;
                }
            }
        }


        private void OnApplicationQuit()
        {
            m_ShuttingDown = true;
        }


        private void OnDestroy()
        {
            m_ShuttingDown = true;
        }
    }
}
