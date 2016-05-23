using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Employee.Common.DBOperation
{
    /// <summary>
    /// 通用的数据库操作
    /// </summary>
    public abstract class DBOper 
    {
        /// <summary>
        /// 数据库操作类配置
        /// </summary>
        private enum DBOperType
        {
            /// <summary>
            /// oracle数据库操作
            /// </summary>
            Oracle,
            /// <summary>
            /// sql server数据库操作
            /// </summary>
            SqlServer
        }

        /// <summary>
        /// 配置文件的字符串的name名称
        /// </summary>
        public static readonly String ConnetionStringString = "Conn";

        /// <summary>
        /// 数据库类型
        /// </summary>
        public static readonly String DBType = "DBType"; 
          

        /// <summary>
        /// 加载数据操作类
        /// </summary>
        public static IDBOper DBOperSingle
        {
            get
            { 
                //读取数据库操作类型
                String DBTypeValue = System.Configuration.ConfigurationManager.AppSettings[DBType];
                //读取数据库连接
                String DBConnString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnetionStringString].ConnectionString;
                IDBOper dbOper = null; 
                //根据数据库类型 返回相对应的数据库操作类型
                if (DBTypeValue == DBOperType.Oracle.ToString())
                {
                    dbOper = new DBOperOracle(DBConnString);
                }
                else if (DBTypeValue == DBOperType.SqlServer.ToString())
                {
                    dbOper = new DBOperSqlServer(DBConnString);
                } 
  
                return dbOper;
            }
        } 
    }
} 
        