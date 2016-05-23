using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Employee.Common;

namespace Employee.Dal.Exception
{
    [Serializable]
    /// <summary>
    /// 数据库操作异常
    /// </summary>
    public class DalException : System.Exception, ISerializable 
    {
        public DalException(String strInfo)
            : base(strInfo)
        {
            Common.LogTxt.Log(LogTxt.FileType.数据库异常日志, this);
        }

        public DalException(System.Exception e)
            : base(e.Message,e)
        {
            Common.LogTxt.Log(LogTxt.FileType.数据库异常日志, e);
        }


        //实现了ISerializable接口的类必须包含有序列化构造函数，否则会出错。
        public DalException(SerializationInfo info, StreamingContext context)
            : this(info.GetString("ExceptionMessage"))
        { 
        }
  
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ExceptionMessage", this.Message);
        }

        public override String ToString()
        {
            return "数据库操作发生异常";
        }
    }
}
