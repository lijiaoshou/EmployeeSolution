using Employee.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Employee.Common.DBOperation;
namespace Employee.Dal
{
    public class EmployeeNewInfoDal
    {
       public List<EmployeeNewInfo> GetPageList(int start, int end)
       {
           try
           {
                //获取数据库操作接口
                IDBOper dbOper = DBOper.DBOperSingle;
                //打开数据库连接
                using (IDbConnection conn = dbOper.OpenConn())
                {
                    string sql = "select * from (select row_number() over(order by id) as num,* from T_News) as t where t.num>=@start and t.num<=@end";
                    IDataParameter[] paras = new IDataParameter[] 
                    {
                      dbOper.GetDataParameter("@start",start),
                      dbOper.GetDataParameter("@end",end)
                    };
                    DataTable dt = dbOper.Query(sql, paras, conn);
                    List<EmployeeNewInfo> list = null;
                    if (dt.Rows.Count > 0)
                    {
                        list = new List<EmployeeNewInfo>();
                        EmployeeNewInfo newInfo = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            newInfo = new EmployeeNewInfo();
                            LoadEntity(row, newInfo);
                            list.Add(newInfo);
                        }
                    }
                    return list;
                }
           }
           catch(System.Exception ex)
           {
           throw new Exception.DalException(ex.Message);
           }
       }
       private void LoadEntity(DataRow row, EmployeeNewInfo newInfo)
       {
           newInfo.Id = Convert.ToInt32(row["ID"]);
           newInfo.Author = row["Author"] != DBNull.Value ? row["Author"].ToString() : string.Empty;
           newInfo.Title = row["Title"] != DBNull.Value ? row["Title"].ToString() : string.Empty;
           newInfo.Msg = row["Msg"] != DBNull.Value ? row["Msg"].ToString() : string.Empty;
           newInfo.ImagePath = row["ImagePath"] != DBNull.Value ? row["ImagePath"].ToString() : string.Empty;
           newInfo.SubDateTime = Convert.ToDateTime(row["SubDateTime"]);
       }

       /// <summary>
       /// 获取总的记录数
       /// </summary>
       /// <returns></returns>
       public int GetRecordCount()
       {
           try
           {
                //获取数据库操作接口
                IDBOper dbOper = DBOper.DBOperSingle;
                //打开数据库连接
                using (IDbConnection conn = dbOper.OpenConn())
                {
                    string sql = "select count(*) from T_News";
                    return Convert.ToInt32(dbOper.ExecuteScalar(sql,null,conn,null));
                }
           }
           catch (System.Exception ex)
           {
               throw new Exception.DalException(ex.Message);
           }
       }
       /// <summary>
       /// 获取一条记录
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public EmployeeNewInfo GetModel(int id)
       {
           try
           {
               //获取数据库操作接口
               IDBOper dbOper = DBOper.DBOperSingle;
               //打开数据库连接
               using (IDbConnection conn = dbOper.OpenConn())
               {
                   string sql = "select * from T_News where id=@id";
                   IDataParameter[] paras = new IDataParameter[] 
                    {
                      dbOper.GetDataParameter("@id",id),
                    };
                   DataTable dt = dbOper.Query(sql, paras, conn);
                   EmployeeNewInfo newInfo = null;
                   if (dt.Rows.Count > 0)
                   {
                       newInfo = new EmployeeNewInfo();
                       LoadEntity(dt.Rows[0], newInfo);
                   }
                   return newInfo;
               }
           }
           catch (System.Exception ex)
           {
               throw new Exception.DalException(ex.Message);
           }
       }
       /// <summary>
       /// 删除一条记录
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public bool DeleteInfo(int id)
       {
           try
           {
               //获取数据库操作接口
               IDBOper dbOper = DBOper.DBOperSingle;
               //打开数据库连接
               using (IDbConnection conn = dbOper.OpenConn())
               {
                   string sql = "delete from T_News where id=@id";
                   IDataParameter[] paras = new IDataParameter[] 
                    {
                      dbOper.GetDataParameter("@id",id),
                    };
                   return dbOper.Exce(sql,paras,conn);
               }
           }
           catch (System.Exception ex)
           {
               throw new Exception.DalException(ex.Message);
           }
       }

       /// <summary>
       /// 添加一条记录
       /// </summary>
       /// <param name="newInfo"></param>
       /// <returns></returns>
       public bool AddInfo(EmployeeNewInfo newInfo)
       {
           try
           {
               //获取数据库操作接口
               IDBOper dbOper = DBOper.DBOperSingle;
               //打开数据库连接
               using (IDbConnection conn = dbOper.OpenConn())
               {
                   string sql = "insert into T_News(Author,Title,Msg,ImagePath,SubDateTime) values(@Author,@Title,@Msg,@ImagePath,@SubDateTime)";
                   IDataParameter[] paras = new IDataParameter[] 
                    {
                      dbOper.GetDataParameter("@Author",newInfo.Author),
                      dbOper.GetDataParameter("@Title",newInfo.Title),
                      dbOper.GetDataParameter("@Msg",newInfo.Msg),
                      dbOper.GetDataParameter("@ImagePath",newInfo.ImagePath),
                      dbOper.GetDataParameter("@SubDateTime",newInfo.SubDateTime)
                    };
                   return dbOper.Exce(sql, paras, conn);
               }
           }
           catch (System.Exception ex)
           {
               throw new Exception.DalException(ex.Message);
           }
       }

    }
}
