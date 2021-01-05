using MongoDB.Bson.IO;
using MongoDB.Driver;
using MongoDBUtils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class A
    {
        public static int X = 2;
        static A()
        {
            X = B.Y + 1;
        }
    }
    class B
    {
        public static int Y = A.X + 1;
        static B() { }
    }

    class Program
    {

        static void Main(string[] args)
        {
            {
                //Console.WriteLine("X={0},Y={1}", A.X, B.Y);//43
                Console.WriteLine("X={0},Y={1}", B.Y, A.X);
                Console.Read();
            }
           
           var argsString= string.Join(",", args);

            BasicContext Context = new BasicContext("LVYANDDDDDDDDDD");
            var model = new User()
            {
                Name = "小七子",
                Cardcertificate = "123456789",
                Gender = Gender.Boy,
                Houses = new System.Collections.Generic.List<House>() { new House() { Adress = "中关村110号双拼别墅" + argsString } }
            };
            var strModel = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(strModel);




            Context.Users.Add(new User()
            {
                Name = "小七子",
                Cardcertificate = "123456789",
                Gender = Gender.Boy,
                Houses = new System.Collections.Generic.List<House>() { new House() { Adress = "中关村110号双拼别墅" + argsString } }
            });

            Context.Users.Where(o => o.Cardcertificate == "110").ToList();

            Context.Users.Update(new User() { });

            Context.Users.Remove(new User() { });

            Context.Users.Where().Select(o => o.Name).ToList();


            Console.Read();

        }
    }
}
