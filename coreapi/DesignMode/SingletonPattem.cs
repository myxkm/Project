using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreapi.DesignMode
{
    #region 饿汉式

    /// <summary>
    /// 设计模式之 单例模式  利用静态变量
    /// </summary>
    public class SingletonPattem
    {
        private SingletonPattem() { }
        private static readonly SingletonPattem _instance = new SingletonPattem();
        /// <summary>
        /// 利用静态变量
        /// </summary>
        /// <returns></returns>
        public static SingletonPattem GetInstance()
        {
            return _instance;
        }
    }

    /// <summary>
    /// 设计模式之 单例模式 
    /// </summary>
    public class SingletonPattem0
    {
        private SingletonPattem0() { }
        private static SingletonPattem0 _instance = null;
        static SingletonPattem0()
        {
            _instance = new SingletonPattem0();
        }
        /// <summary>
        /// 利用静态构造函数
        /// </summary>
        /// <returns></returns>
        public static SingletonPattem0 GetInstance()
        {
            return _instance;
        }
    }

    #endregion

    #region 懒汉式
    /// <summary>
    /// 设计模式之 单例模式 
    /// </summary>
    public class SingletonPattem1
    {
        private readonly static object obj = new object();
        private static SingletonPattem1 _instance = null;
        private SingletonPattem1() { }
        
        /// <summary>
        /// 线程安全 双if_lock
        /// </summary>
        /// <returns></returns>
        public static SingletonPattem1 GetInstance()
        {
            if (_instance == null)
            {
                lock (obj)
                {
                    if (_instance == null)
                    {
                        _instance = new SingletonPattem1();
                    }
                }
            }
            return _instance;
        }
    }
    #endregion
}
