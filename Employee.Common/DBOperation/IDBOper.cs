using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Employee.Common.DBOperation
{
    /// <summary>
    /// 数据库操作接口
    /// </summary>
    public interface IDBOper
    {
        /// <summary>
        /// 数据库连接串
        /// </summary>
        String ConnetionString { set; get; }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <returns>返回数据库连接对象</returns>
        IDbConnection OpenConn();

        /// <summary>
        /// 获取对于操作的数据库参数类型
        /// </summary>
        /// <returns>参数类型接口</returns>
        IDataParameter GetDataParameter();
         
        /// <summary>
        /// 用参数名称和新 System.Data.SqlClient.SqlParameter 的一个值初始化 System.Data.SqlClient.SqlParameter
        ///类的新实例。
        /// </summary>
        /// <param name="parameterName"> 要映射的参数的名称</param>
        /// <param name="value">一个 System.Object，它是 System.Data.SqlClient.SqlParameter 的值。</param>
        /// <returns>参数类型接口</returns>
        IDataParameter GetDataParameter(string parameterName, object value);

        /// <summary>
        /// 用参数名称和新 System.Data.SqlClient.SqlParameter 的一个值初始化 System.Data.SqlClient.SqlParameter
        ///类的新实例。
        /// </summary>
        /// <param name="parameterName"> 要映射的参数的名称</param>
        /// <param name="direction">传入参数的类型</param>
        /// <param name="value">一个 System.Object，它是 System.Data.SqlClient.SqlParameter 的值。</param>
        /// <returns>参数类型接口</returns>
        IDataParameter GetDataParameter(string parameterName,DbType dbtype, ParameterDirection direction, object value);

        /// <summary>
        /// 执行read
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="paras">执行参数</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>返回读取方法</returns>
        IDataReader Read(String sql, IDataParameter[] paras, IDbConnection conn);

        /// <summary>
        /// 执行read
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>返回读取方法</returns>
        IDataReader Read(String sql, IDbConnection conn);
        
        
        /// <summary>
        /// 执行read
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="paras">执行参数</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="sqlTrans">事物</param>
        /// <returns>执行成功返回true</returns>
        IDataReader Read(String sql, IDataParameter[] paras, IDbConnection conn, IDbTransaction sqlTrans);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>执行成功返回true</returns>
        bool Exce(String sql, IDbConnection conn);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="sqlTrans">事物</param>
        /// <returns>执行成功返回true</returns>
        bool Exce(String sql, IDbConnection conn, IDbTransaction sqlTrans);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">执行的sql语句<</param>
        /// <param name="paras">执行参数</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>执行成功返回true</returns>
        bool Exce(String sql, IDataParameter[] paras, IDbConnection conn);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="paras">执行参数</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="sqlTrans">事物</param>
        /// <returns>执行成功返回true</returns>
        bool Exce(String sql, IDataParameter[] paras, IDbConnection conn, IDbTransaction sqlTrans);

        /// <summary>
        /// 执行查询sql语句
        /// </summary>
        /// <param name="sql">查询sql语句</param>
        /// <param name="paras">查询参数</param>
        /// <param name="conn">数据库连接</param>
        /// <returns></returns>
        DataTable Query(string sql, IDataParameter[] paras, IDbConnection conn);

        
        /// <summary>
        /// 执行数据库的ExecuteScalar方法
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数，如果没有可传null</param>
        /// <param name="conn">数据库连接 </param>
        /// <param name="sqlTrans">存储过程</param>
        /// <returns></returns>
        object ExecuteScalar(String sql, IDataParameter[] paras, IDbConnection conn, IDbTransaction sqlTrans);

        /// <summary>
        /// 执行存储过程 
        /// </summary>
        /// <param name="sql">储过程名称<</param>
        /// <param name="paras">执行参数</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>执行成功返回true</returns>
        bool ExceProc(String procName, IDataParameter[] paras, IDbConnection conn);

        /// <summary>
        /// 执行存储过程 
        /// </summary>
        /// <param name="procName">储过程名称<</param>
        /// <param name="paras">执行参数</param>
        ///<param name="tx">数据库事务</param>
        /// <returns>执行成功返回true</returns>
        bool ExceProc(String procName, IDataParameter[] paras, IDbTransaction tx);

    }
}
