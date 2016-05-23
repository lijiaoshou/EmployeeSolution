using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee.Dal;
using Employee.Entity;

namespace Employee.Bll
{
    public class UserInfoService
    {
        public UserInfo GetUserInfo(string username, string password)
        {
            try
            {
                UserInfoDal dal = new UserInfoDal();
                return dal.GetUser(username, password);
            }
            catch (System.Exception ex)
            {
                throw new Exception.BllException(ex.Message);
            }
        }

        /// <summary>
        /// 注册账户
        /// </summary>
        /// <param name="userinfo">注册账户实体</param>
        /// <returns>是否注册成功</returns>
        public bool AddUserInfo(UserInfo userinfo)
        {
            try
            {
                UserInfoDal userinfoDal = new UserInfoDal();
                return userinfoDal.AddUser(userinfo);
            }
            catch (System.Exception ex)
            {
                throw new Exception.BllException(ex.Message);
            }
        }
    }
}
