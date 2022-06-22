using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TEngine
{
    public static class ConfigHelper
    {
        private static string m_RootPath = string.Empty;
        public static string RootPath
        {
            get
            {
                //return System.Environment.CurrentDirectory;

                if (string.IsNullOrEmpty(m_RootPath))
                {
                    m_RootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "//Config//";
                }
                return m_RootPath;
            }
        }

        /// <summary>
        /// 获取到本地的Json文件并且解析返回对应的json字符串
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        public static string GetJsonFile(string filepath)
        {
            filepath = string.Concat(RootPath,filepath);
            string json = string.Empty;
            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    json = sr.ReadToEnd().ToString();
                }
            }
            return json;
        }
    }
}
