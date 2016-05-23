using Employee.Common.DBOperation;
using Employee.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Dal
{
    public class UserInfoDal
    {
        public UserInfo GetUser(string userName, string userPwd)
        {
            try
            {
                //获取数据库操作接口
                IDBOper dbOper = DBOper.DBOperSingle;
                //打开数据库连接
                using (IDbConnection conn = dbOper.OpenConn())
                {
                    string sql = "select * from T_UserInfo where UserName=@UserName and UserPwd=@UserPwd";
                    IDataParameter[] paras = new IDataParameter[] 
                    {
                      dbOper.GetDataParameter("@UserName",userName),
                      dbOper.GetDataParameter("@UserPwd",userPwd)
                    };
                    //读取数据
                    DataTable dt = dbOper.Query(sql, paras, conn);
                    UserInfo userInfo = null;
                    if (dt.Rows.Count > 0)
                    {
                        userInfo = new UserInfo();
                        LoadEntity(userInfo, dt.Rows[0]);
                    }
                    return userInfo;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DalException(ex.Message);
            }
           
        }

        private void LoadEntity(UserInfo userInfo, DataRow row)
        {
            try
            {
                userInfo.Id = Convert.ToInt32(row["Id"]);
                userInfo.UserName = row["UserName"] != DBNull.Value ? row["UserName"].ToString() : string.Empty;
                userInfo.UserPwd = row["UserPwd"] != DBNull.Value ? row["UserPwd"].ToString() : string.Empty;
                userInfo.UserMail = row["UserMail"] != DBNull.Value ? row["UserMail"].ToString() : string.Empty;
                userInfo.RegTime = Convert.ToDateTime(row["RegTime"]);
            }
            catch (System.Exception ex)
            {
                throw new Exception.DalException(ex.Message);
            } 
        }

        /// <summary>
        /// 注册一个用户
        /// </summary>
        /// <param name="newInfo"></param>
        /// <returns></returns>
        public bool AddUser(UserInfo userinfo)
        {
            try
            {
                //获取数据库操作接口
                IDBOper dbOper = DBOper.DBOperSingle;
                //打开数据库连接
                using (IDbConnection conn = dbOper.OpenConn())
                {
                    string sql = "insert into T_UserInfo(username,userpwd,usermail,regtime) values(@UserName,@UserPwd,@UserMail,@RegTime)";
                    IDataParameter[] paras = new IDataParameter[] 
                    {
                      dbOper.GetDataParameter("@UserName",userinfo.UserName),
                      dbOper.GetDataParameter("@UserPwd",userinfo.UserPwd),
                      dbOper.GetDataParameter("@UserMail",userinfo.UserMail),
                      dbOper.GetDataParameter("@RegTime",userinfo.RegTime)
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
