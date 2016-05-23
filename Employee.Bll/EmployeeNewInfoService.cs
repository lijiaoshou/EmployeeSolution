using Employee.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee.Dal;

namespace Employee.Bll
{
    public class EmployeeNewInfoService
    {
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageIndex">当前页码值</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <returns></returns>
       public List<EmployeeNewInfo> GetPageList(int pageIndex, int pageSize)
       {
           try
           {
               EmployeeNewInfoDal EmpNewInfoDal = new EmployeeNewInfoDal();
               int start = (pageIndex - 1) * pageSize + 1;
               int end = pageIndex * pageSize;
               List<EmployeeNewInfo> list = EmpNewInfoDal.GetPageList(start, end);
               return list;
           }
           catch (System.Exception ex)
           {
               throw new Exception.BllException(ex.Message);
           }
       }
       /// <summary>
       /// 获取总的页数
       /// </summary>
       /// <param name="pageSize"></param>
       /// <returns></returns>
       public int GetPageCount(int pageSize)
       {
           try
           {
               EmployeeNewInfoDal EmpNewInfoDal = new EmployeeNewInfoDal();
               int recordCount = EmpNewInfoDal.GetRecordCount();
               int pageCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
               return pageCount;
           }
           catch (System.Exception ex)
           {
               throw new Exception.BllException(ex.Message);
           }
       }
       public EmployeeNewInfo GetModel(int id)
       {
           try
           {
               EmployeeNewInfoDal EmpNewInfoDal = new EmployeeNewInfoDal();
               return EmpNewInfoDal.GetModel(id);
           }
           catch (System.Exception ex)
           {
               throw new Exception.BllException(ex.Message);
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
               EmployeeNewInfoDal EmpNewInfoDal = new EmployeeNewInfoDal();
               return EmpNewInfoDal.DeleteInfo(id);
           }
           catch (System.Exception ex)
           {
               throw new Exception.BllException(ex.Message);
           }
       }
       public bool AddInfo(EmployeeNewInfo newInfo)
       {
           try
           {
               EmployeeNewInfoDal EmpNewInfoDal = new EmployeeNewInfoDal();
               if (newInfo.ImagePath == null)
               {
                   newInfo.ImagePath = "";
               }
               return EmpNewInfoDal.AddInfo(newInfo);
           }
           catch (System.Exception ex)
           {
               throw new Exception.BllException(ex.Message);
           }

       }
    }
}
