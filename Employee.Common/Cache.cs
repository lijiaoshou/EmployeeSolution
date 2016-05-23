using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employee.Common
{
    /// <summary>
    /// 缓存池
    /// </summary> 
    public class Cache
    {
        /// <summary>
        /// 缓存字典
        /// </summary>
        private static Dictionary<Object, Object> DicCache = new Dictionary<object, object>();

        /// <summary>
        /// 缓存字典的插入时间
        /// </summary>
        private static Dictionary<Object, DateTime> DicCacheInsertDateTime = new Dictionary<object, DateTime>();

        /// <summary>
        /// 缓存过期分钟
        /// </summary>
        private int expiredMin = 30;

        /// <summary>
        /// 缓存过期分钟
        /// </summary>
        public int ExpiredMin
        {
            get { return expiredMin; }
            set { expiredMin = value; }
        }

        /// <summary>
        /// 缓存的单例
        /// </summary>
        private static readonly Cache cache = new Cache();

        /// <summary>
        /// 缓存的单例
        /// </summary>
        public static Cache CacheSingle
        {
            get
            { 
                return cache;
            }
        }

        private Cache()
        {
        }
         
        /// <summary>
        /// 设置缓存
        /// 如果该关键已经存在 就更新该缓存
        /// </summary>
        /// <param name="key">缓存关键字</param>
        /// <param name="value">缓存内容</param>
        /// <returns>设置成功返回true</returns>
        public bool SetCache(Object key,Object value)
        {
            lock (this)
            {
                DicCache[key] = value;
                DicCacheInsertDateTime[key] = DateTime.Now;
            }
            return true;
        }
         
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存关键字</param>
        /// <returns>缓存内容,如果为获取到为null</returns>
        public object GetCache(Object key )
        {
            lock (this)
            {
                //如果cache 里面没有 就返回null
                if (!DicCache.ContainsKey(key))
                    return null;

                //查看cache是否过期  过期的话，就删除该关键字的cache
                if (IsExpired(key))
                {
                    DicCache.Remove(key);
                    DicCacheInsertDateTime.Remove(key);
                    return null;
                }

                return DicCache[key];
            } 
        }

        /// <summary>
        /// 默认固定缓存30分钟自动过期，这里不检查cache里面是否有关键字
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>未过期返回false，缓存过期返回true</returns>
        private bool IsExpired(Object key)
        {
            //与当前时间减去缓存插入的时间的数值，以此判断缓存是否过期
            TimeSpan ts = DateTime.Now - DicCacheInsertDateTime[key];
            if (ts.TotalMinutes > expiredMin)
                return true;
            return false;
        }
    }
}
