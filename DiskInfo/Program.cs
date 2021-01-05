using System;
using System.IO;
using System.Management;

namespace DiskInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            GetDiskInfo();
            Console.ReadLine();
        }

        public static void GetDiskInfo()
        {
            var strSaveFolder = @"D:\";
            try
            {
                //获得盘符HARDDISK
                var index = strSaveFolder.Trim().IndexOf(':');
                var HARDDISK = strSaveFolder.Trim().Substring(0, index + 1);

                SelectQuery selectQuery = new SelectQuery("select * from win32_logicaldisk");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(selectQuery);
                ManagementObjectCollection diskcollection = searcher.Get();
                if (diskcollection != null && diskcollection.Count > 0)
                {
                    foreach (ManagementObject item in searcher.Get())
                    {
                        int nType = Convert.ToInt32(item["DriveType"]);
                        if (nType != Convert.ToInt32(DriveType.Fixed))
                        {
                            continue;
                        }
                        else
                        {
                            if (item["DeviceID"].ToString().ToUpper() == HARDDISK)
                            {
                                double bytes = Convert.ToDouble(item["FreeSpace"]);

                                var result = bytes / Math.Pow(1024, 3);

                                if (result < 2)
                                {
                                    //return false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //return false;
            }

            //return true;
        }
    }
}
