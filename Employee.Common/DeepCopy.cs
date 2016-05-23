using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employee.Common
{
    /// <summary>
    /// 深拷贝
    /// </summary>
    public class DeepCopy
    {
        public static Object Copy(Object source)
        {
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            formatter.Serialize(memoryStream, source);
            memoryStream.Position = 0;
            return formatter.Deserialize(memoryStream);
        }

    }
}
