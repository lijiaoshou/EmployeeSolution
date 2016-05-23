using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employee.Common
{
    public class ProcessInfo
    {
        public static bool HasCurProcess()
        {
            String proName = System.Diagnostics.Process.GetCurrentProcess().ProcessName ; 
            IList<System.Diagnostics.Process> list = 
                System.Diagnostics.Process.GetProcesses().Where(v => v.ProcessName == proName).ToList() as IList<System.Diagnostics.Process>;
            if (list != null && list.Count > 1)
                return true;

            return false;
        }

        public static string GetCurProcessName()
        {
            return System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        }


        public static void KillSelf()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
