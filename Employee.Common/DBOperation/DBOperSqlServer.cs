using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Employee.Common.DBOperation
{
    /// <summary>
    /// sql server数据库操作
    /// </summary>
    public class DBOperSqlServer :IDBOper
    {        
        public DBOperSqlServer()
        {
        }
        public DBOperSqlServer(String connection)
        {
            this.ConnetionString = connection;
        }

        /// <summary>
        /// 数据库连接串
        /// </summary>
        public String ConnetionString { set; get; }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <returns>返回数据库连接对象</returns>
        public IDbConnection OpenConn()
        {
            SqlConnection sqlConn = new SqlConnection(ConnetionString);
            sqlConn.Open();
            return sqlConn;
        }

        /// <summary>
        /// 获取对于操作的数据库参数类型
        /// </summary>
        /// <returns>参数类型接口</returns>
        public IDataParameter GetDataParameter()
        {
            return new SqlParameter();
        }

        /// <summary>
        /// 用参数名称和新 System.Data.SqlClient.SqlParameter 的一个值初始化 System.Data.SqlClient.SqlParameter
        ///类的新实例。
        /// </summary>
        /// <param name="parameterName"> 要映射的参数的名称</param>
        /// <param name="value">一个 System.Object，它是 System.Data.SqlClient.SqlParameter 的值。</param>
        /// <returns>参数类型接口</returns>
        public IDataParameter GetDataParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, value);
        }

        public IDataParameter GetDataParameter(string parameterName,DbType dbtype, ParameterDirection direction, object value)
        {
            return null;
        }
         
        /// <summary>
        /// 执行read
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="paras">执行参数</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>返回读取方法</returns>
        public IDataReader Read(String sql, IDataParameter[] paras, IDbConnection conn)
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Connection = conn as SqlConnection;
            comm.CommandType = System.Data.CommandType.Text;
            foreach (SqlParameter para in paras)
                comm.Parameters.Add(para);
            return comm.ExecuteReader();
        }


        /// <summary>
        /// 执行read
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>返回读取方法</returns>
        public IDataReader Read(String sql, IDbConnection conn)
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Connection = conn as SqlConnection;
            comm.CommandType = System.Data.CommandType.Text;
            return comm.ExecuteReader();
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>执行成功返回true</returns>
        public bool Exce(String sql, IDbConnection conn)
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Connection = conn as SqlConnection;
            comm.CommandType = System.Data.CommandType.Text;
            return comm.ExecuteNonQuery() >= 0 ? true : false;
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="sqlTrans">事物</param>
        /// <returns>执行成功返回true</returns>
        public bool Exce(String sql, IDbConnection conn, IDbTransaction sqlTrans)
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Connection = conn as SqlConnection;
            comm.CommandType = System.Data.CommandType.Text;
            comm.Transaction = sqlTrans as SqlTransaction;
            return comm.ExecuteNonQuery() >= 0 ? true : false;
        }


        /// <summary>
        /// 执行read
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="paras">执行参数</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="sqlTrans">事物</param>
        /// <returns>执行成功返回true</returns>
        public IDataReader Read(String sql, IDataParameter[] paras, IDbConnection conn, IDbTransaction sqlTrans)
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Connection = conn as SqlConnection;
            comm.CommandType = System.Data.CommandType.Text;

            if (sqlTrans != null)
            {
                comm.Transaction = sqlTrans as SqlTransaction;
            }

            if (paras != null)
            {
                foreach (SqlParameter para in paras)
                {
                    if (para.Value == null)
                        para.Value = DBNull.Value;
                    comm.Parameters.Add(para);
                }
            }
            return comm.ExecuteReader();
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">执行的sql语句<</param>
        /// <param name="paras">执行参数</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>执行成功返回true</returns>
        public bool Exce(String sql, IDataParameter[] paras, IDbConnection conn)
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Connection = conn as SqlConnection;
            comm.CommandType = System.Data.CommandType.Text;
            foreach (SqlParameter para in paras)
                comm.Parameters.Add(para);
            return comm.ExecuteNonQuery() >= 0 ? true : false;
        }

        /// <summary>
        /// 执行查询sql语句
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="paras">查询参数</param>
        /// <param name="conn">数据库连接</param>
        /// <returns></returns>
        public DataTable Query(string sql, IDataParameter[] paras, IDbConnection conn)
        {
            using (SqlDataAdapter apter = new SqlDataAdapter(sql, conn as SqlConnection))
            {
                if (paras != null)
                {
                    apter.SelectCommand.Parameters.AddRange(paras);
                }
                DataTable da = new DataTable();
                apter.Fill(da);
                return da;
            }
        }


        /// <summary>
        /// 执行数据库的ExecuteScalar方法
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数，如果没有可传null</param>
        /// <param name="conn">数据库连接 </param>
        /// <param name="sqlTrans">存储过程</param>
        /// <returns></returns>
        public object ExecuteScalar(String sql, IDataParameter[] paras, IDbConnection conn, IDbTransaction sqlTrans)
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Connection = conn as SqlConnection;
            comm.CommandType = System.Data.CommandType.Text;

            if (sqlTrans != null)
                comm.Transaction = sqlTrans as SqlTransaction;

            if (paras != null)
            {
                foreach (SqlParameter para in paras)
                {
                    if (para.Value == null)
                        para.Value = DBNull.Value;
                    comm.Parameters.Add(para);
                }
            }
            return comm.ExecuteScalar();
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="paras">执行参数</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="sqlTrans">事物</param>
        /// <returns>执行成功返回true</returns>
        public bool Exce(String sql, IDataParameter[] paras, IDbConnection conn, IDbTransaction sqlTrans)
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Connection = conn as SqlConnection;
            comm.CommandType = System.Data.CommandType.Text;
            comm.Transaction = sqlTrans as SqlTransaction;
            foreach (SqlParameter para in paras)
                comm.Parameters.Add(para);
            return comm.ExecuteNonQuery() >= 0 ? true : false;
        }

        /// <summary>
        /// 执行存储过程 
        /// </summary>
        /// <param name="sql">储过程名称<</param>
        /// <param name="paras">执行参数</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>执行成功返回true</returns>
        public bool ExceProc(String procName, IDataParameter[] paras, IDbConnection conn)
        {
            return false;
        }

        public bool ExceProc(string procName, IDataParameter[] paras, IDbTransaction tx)
        {
            return false;
        }
    }
}
