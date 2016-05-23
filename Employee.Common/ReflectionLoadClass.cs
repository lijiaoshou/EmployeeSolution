using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Employee.Common
{
    /// <summary>
    /// 通过反射加载类
    /// </summary>
    public class ReflectionLoadClass
    {
        /// <summary>
        /// 获取class的type
        /// </summary>
        /// <param name="dllName">类库名称</param>
        /// <param name="allClassName">含命名空间，类名</param>
        /// <returns>返回Type</returns>
        public static Type GetType(String dllName, String allClassName)
        {
            Assembly assembly = Assembly.Load(dllName);
            return assembly.GetType(allClassName);
        }

        /// <summary>
        /// 反射对实例化类
        /// </summary>
        /// <param name="dllName">类库名称</param>
        /// <param name="allClassName">含命名空间，类名</param>
        /// <returns>返回实例化好的类</returns>
        public static Object Load(String dllName,String allClassName)
        {
            return Activator.CreateInstance(GetType(dllName, allClassName));
        }
        
        /// <summary>
        /// 反射对实例化类
        /// </summary>
        /// <param name="dllName">类库名称</param>
        /// <param name="allClassName">含命名空间，类名</param>
        /// <param name="constructorParames">构造函数的参数</param>
        /// <returns>返回实例化好的类</returns>
        public static Object Load(String dllName, String allClassName, object[] constructorParames)
        { 
            return Activator.CreateInstance(GetType(dllName, allClassName), constructorParames);
        }
    }
}
