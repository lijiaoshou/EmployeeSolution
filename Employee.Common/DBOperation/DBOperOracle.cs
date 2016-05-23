using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace Employee.Common.DBOperation
{
    public class DBOperOracle :   IDBOper
    {
        public DBOperOracle()
        {
        }
        public DBOperOracle(String connection)
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
            //ConnetionString = @"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))
            //(CONNECT_DATA=(SERVICE_NAME=JLJK90)));User Id=gacrj;Password=gacrj;"; 
            OracleConnection sqlConn = new OracleConnection(ConnetionString); 
            sqlConn.Open();
            return sqlConn;
        }

        /// <summary>
        /// 获取对于操作的数据库参数类型
        /// </summary>
        /// <returns>参数类型接口</returns>
        public IDataParameter GetDataParameter()
        {
            return new OracleParameter();
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
            return new OracleParameter(parameterName, value);
        }

        /// <summary>
        /// 初始化数据库参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="direction"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IDataParameter GetDataParameter(string parameterName,DbType dbtype, ParameterDirection direction, object value)
        {
            OracleParameter para = new OracleParameter();
            para.ParameterName = parameterName;
            if (dbtype == DbType.String)
            {
                para.OracleType = OracleType.VarChar;
                para.Size = 200;
            }
            else
            {
                para.DbType = dbtype;
            }
            para.Direction = direction;
            para.Value = value;

            return para;
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
            return Read(sql, paras, conn, null);
        }


        /// <summary>
        /// 执行read
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>返回读取方法</returns>
        public IDataReader Read(String sql, IDbConnection conn)
        { 
            return Read(sql,null,conn,null);
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
            OracleCommand comm = new OracleCommand();
            comm.CommandText = sql;
            comm.Connection = conn as OracleConnection;
            comm.CommandType = System.Data.CommandType.Text;

            if (sqlTrans != null)
            {
                comm.Transaction = sqlTrans as OracleTransaction;
            }

            if (paras != null)
            {
                foreach (OracleParameter para in paras)
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
        /// <param name="sql">执行的sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>执行成功返回true</returns>
        public bool Exce(String sql, IDbConnection conn)
        {
            return Exce(sql, null, conn, null);
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
            return Exce(sql, null, conn, sqlTrans);
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
            return Exce(sql,paras,conn,null);
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
            OracleCommand comm = new OracleCommand();
            comm.CommandText = sql;
            comm.Connection = conn as OracleConnection;
            comm.CommandType = System.Data.CommandType.Text;

            if (sqlTrans != null)
            {
                comm.Transaction = sqlTrans as OracleTransaction;
            }

            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
                    if (para.Value == null)
                        para.Value = DBNull.Value;
                    comm.Parameters.Add(para);
                }
            }
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
            using (OracleDataAdapter apter = new OracleDataAdapter(sql, conn as OracleConnection))
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
            OracleCommand comm = new OracleCommand();
            comm.CommandText = sql;
            comm.Connection = conn as OracleConnection;
            comm.CommandType = System.Data.CommandType.Text;

            if (sqlTrans!=null)
                comm.Transaction = sqlTrans as OracleTransaction;

            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
                    if (para.Value == null)
                        para.Value = DBNull.Value;
                    comm.Parameters.Add(para);
                }
            }
            return comm.ExecuteScalar() ;
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
            OracleCommand comm = new OracleCommand();
            comm.CommandText = procName;
            comm.Connection = conn as OracleConnection;
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
                    if (para.Value == null)
                        para.Value = DBNull.Value;
                    comm.Parameters.Add(para);
                }
            }
            comm.ExecuteNonQuery();
            return true;
        }

        public bool ExceProc(string procName, IDataParameter[] paras, IDbTransaction tx)
        {
            OracleCommand comm = new OracleCommand();
            comm.CommandText = procName;
            comm.Connection = tx.Connection as OracleConnection;
            comm.Transaction = tx as OracleTransaction;
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
                    if (para.Value == null)
                        para.Value = DBNull.Value;
                    comm.Parameters.Add(para);
                }
            }
            comm.ExecuteNonQuery();
            return true;
        }
    }
}
