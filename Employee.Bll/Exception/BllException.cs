using System;
using System.Collections.Generic; 
using System.Text;
using System.Runtime.Serialization;

namespace Employee.Bll.Exception
{
    [Serializable]
    /// <summary>
    /// 业务层异常
    /// </summary>
    public class BllException:System.Exception,ISerializable 
    {

        public BllException(String strInfo,bool log)
            : base(strInfo)
        {
            if (log)
                Common.LogTxt.Log(Common.LogTxt.FileType.业务处理异常日志, this);
        }

        public BllException(String strInfo)
            : base(strInfo)
        {
            Common.LogTxt.Log(Common.LogTxt.FileType.业务处理异常日志, this);
        }

        public BllException(System.Exception e)
            : base(e.Message,e)
        {
            Common.LogTxt.Log(Common.LogTxt.FileType.业务处理异常日志, e);
        }

        //实现了ISerializable接口的类必须包含有序列化构造函数，否则会出错。
        public BllException(SerializationInfo info, StreamingContext context)
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
            return "业务操作发生异常";
        }
    }
}
