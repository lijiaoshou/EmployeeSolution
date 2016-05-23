using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employee.Common
{
    public class DateFormat
    {
        public static String ConvertZWToMark(String inStr)
        {
            if (String.IsNullOrEmpty(inStr))
                return inStr;

            inStr = inStr.Replace("年", "-");
            inStr = inStr.Replace("月", "-");
            inStr = inStr.Replace("日", "-");
            if (inStr.EndsWith("-"))
                inStr = inStr.Remove(inStr.Length - 1);

            return inStr;
        }


        public static String ConvertZWAddToMonth(String inStr)
        {
            if (String.IsNullOrEmpty(inStr))
                return inStr;

            DateTime dt = DateTime.ParseExact(inStr, "yyyy-MM-dd", null);
            return dt.AddMonths(1).ToString("yyyy-MM-dd");
        }

        #region 分钟转天时分格式
        public static string ConvertDateFormat(string gzsc)
        {
            int totalMinutes = 0;
            try
            {
                totalMinutes = int.Parse(gzsc);
            }
            catch (Exception ex)
            { }

            string str = "";
            int days = totalMinutes / 60 / 24;

            int hours = 0;
            if (days > 0)
                hours = totalMinutes % (60 * 24) / 60;
            else
                hours = totalMinutes / 60;

            int minutes = totalMinutes % 60;

            if (days > 0)
                str = str + days + "天";
            if (hours > 0)
                str = str + hours + "小时";
            if (minutes > 0)
                str = str + minutes + "分钟";
            return str;
        }
        #endregion
    }
}
