using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Employee.Common
{
    /// <summary>
    /// log文本记录
    /// </summary>
    public class LogTxt
    {
        public enum FileType
        {
            Web用户操作日志,
            后台监控调用日志,
            数据库异常日志,
            业务处理异常日志,
            Web前端异常日志,
            后台监控异常日志
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        private static readonly String FILEPATH = ".\\Log\\";

        private static Object lockObject = new object();
        /// <summary>
        /// 锁对象
        /// </summary>
        private static Object LockObject 
        {
            get
            {
                if (lockObject == null)
                    return new Object();
                return lockObject;
            }
        }
         
        private LogTxt()
        {
        }
          
        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="e"></param>
        public static void Log(FileType type, Exception e)
        {
            lock (LockObject)
            { 
                try
                {
                    CreateFilePath();
                    StreamWriter sw = new StreamWriter(new FileStream(GetFileAllPath(type), FileMode.Append, FileAccess.Write));
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    sw.WriteLine("Source");
                    sw.WriteLine(e.Source);
                    sw.WriteLine("StackTrace");
                    sw.WriteLine(e.StackTrace);
                    sw.WriteLine("Message");
                    sw.WriteLine(e.Message);
                    sw.Close();
                }
                catch(Exception ex)
                {
                }
            }
        }


        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="e"></param>
        public static void Log(FileType type, String info)
        {
            lock (LockObject)
            {
                try
                {
                    CreateFilePath();
                    StreamWriter sw = new StreamWriter(new FileStream(GetFileAllPath(type), FileMode.Append, FileAccess.Write));
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    sw.WriteLine(info);
                    sw.Close();
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        private static void CreateFilePath()
        {
            if (!System.IO.Directory.Exists(FILEPATH))
                System.IO.Directory.CreateDirectory(FILEPATH);
        }

        /// <summary>
        /// 根据文件类型返回文件全名称 ，含文件路径
        /// </summary>
        /// <param name="fileType">文件类型</param>
        /// <returns>返回文件全名称</returns>
        private static String GetFileAllPath(FileType fileType)
        {
            String fileAllPath = FILEPATH + fileType.ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd")+".txt";
            return fileAllPath;
        }
    }
}
