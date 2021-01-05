using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disk
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string> {
                @"D:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Backup\BPM_LY\",
                @"D:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Backup\MHappy_Com_ZT\",
                @"D:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Backup\SHORT_LY\",
            };
            foreach (var dir in list)
            {
                string directoryName = Path.GetDirectoryName(dir);
                var files = Directory.GetFiles(directoryName);
                List<FileInfo> fileList = new List<FileInfo> { };
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    fileList.Add(fileInfo);
                }
                var currentDate = DateTime.Now.AddDays(-30);//把30天之前的数据都删除了
                fileList = fileList.Where(t => t.CreationTime <= currentDate).ToList();
                foreach (var item in fileList)
                {
                    File.Delete(item.FullName);
                }

            }
        }
    }
}
