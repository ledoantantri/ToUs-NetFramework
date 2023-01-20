using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToUs.Models
{
    public static partial class AppConfig
    {
        public static class AdminMode
        {
            public class ExcelPath
            {
                public string Path { get; set; }
                public bool IsChoosed { get; set; }
                public string Type { get; set; }

                public ExcelPath(string path, string type)
                {
                    Path = path.Trim();
                    IsChoosed = true;
                    Type = type;
                }
            }

            private static List<ExcelPath> _excelPathChoosed = null;

            public static List<ExcelPath> ExcelPathChoosed
            {
                get
                {
                    if (_excelPathChoosed == null)
                        MessageBox.Show("Bạn chưa chọn file excel nào");
                    return _excelPathChoosed;
                }
                set
                {
                    _excelPathChoosed = value;
                }
            }
        }
    }
}