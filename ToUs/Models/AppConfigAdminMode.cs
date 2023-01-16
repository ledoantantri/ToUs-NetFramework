using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToUs.Models
{
    public static partial class AppConfig
    {
        public static class AdminMode
        {
            private static string _currentExcelPath = null;

            public static string CurrentExcelPath
            {
                get
                {
                    return _currentExcelPath;
                }
                set { _currentExcelPath = value; }
            }
        }
    }
}