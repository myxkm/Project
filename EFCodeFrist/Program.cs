using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFrist
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeFristModel codeFristModel = new CodeFristModel
                ();
            codeFristModel.Database.Log += c => Console.Write(c);
            codeFristModel.OJT_User_Score_Details.Where(c=>c.Id>0);
            Console.Read();

        }
    }
}
