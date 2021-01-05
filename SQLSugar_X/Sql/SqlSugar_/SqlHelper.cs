using SqlSugar;
using SqlSugar.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLSugar_X.Sql.SqlSugar_
{
    [SugarTable("Test")]
    public class Student
    {
        //指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public int Port { get; set; }
        public int Count { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class DbContext
    {
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                //ConnectionString = "server=123.57.163.87;uid=jin;pwd=p@ssw0rd;database=BPM_LY",
                //DbType = DbType.SqlServer,
                ConnectionString = "server=127.0.0.1;uid=root;pwd=ly123456;database=mysql",
               DbType = DbType.MySql,

                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
                SlaveConnectionConfigs = new List<SlaveConnectionConfig>
                {
                    //new SlaveConnectionConfig{  HitRate=10, ConnectionString="" },
                },
                ConfigureExternalServices = new ConfigureExternalServices
                {
                    DataInfoCacheService = new HttpRuntimeCache()
                },

            });
            Db.Ado.IsDisableMasterSlaveSeparation = true;
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
           {
               Console.WriteLine(sql + "\r\n" +
                   Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
               Console.WriteLine();
           };

        }
        //注意：不能写成静态的，不能写成静态的
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
        public SimpleClient<Student> StudentDb { get { return new SimpleClient<Student>(Db); } }//用来处理Student表的常用操作
    }

    public class DemoManager : DbContext//继承DbContext
    {
        //SimpleClient实现查询例子
        public void SearchDemo()
        {
       



            var data1 = StudentDb.GetById(1);//根据ID查询
            var data2 = StudentDb.GetList().OrderBy(t =>Guid.NewGuid()).FirstOrDefault();//查询所有


            var data3 = StudentDb.GetList(it => it.Id == 1).OrderBy(t=>DateTime.Now);  //根据条件查询  
            var data4 = StudentDb.GetSingle(it => it.Id == 1);//根据条件查询一条

            var p = new PageModel() { PageIndex = 1, PageSize = 2 };// 分页查询
            var data5 = StudentDb.GetPageList(it => it.Port == 5001, p).OrderBy(t=>Guid.NewGuid());
            Console.Write(p.PageCount);//返回总数

            var ca = StudentDb.AsQueryable().WithCacheIF(p.PageIndex < 2, 10);

            // 分页查询加排序
            var data6 = StudentDb.GetPageList(it => it.Port == 5001, p, it => it.Port, OrderByType.Asc);
            Console.Write(p.PageCount);//返回总数


            //组装条件查询作为条件实现 分页查询加排序
            List<IConditionalModel> conModels = new List<IConditionalModel>();
            conModels.Add(new ConditionalModel() { FieldName = "id", ConditionalType = ConditionalType.Equal, FieldValue = "1" });//id=1
            var data7 = StudentDb.GetPageList(conModels, p, it => it.Port, OrderByType.Asc);

            //4.9.7.5支持了转换成queryable,我们可以用queryable实现复杂功能
            StudentDb.AsQueryable().Where(x => x.Id == 1).ToList();
        }


        //插入例子
        public void InsertDemo()
        {

            var student = new Student() { Port = 5001, Count = 1 };
            var student1 = new Student() { Port = 5001 };
            var student2 = new Student() { Port = 5001 };
            var student3 = new Student() { Port = 5001 };
            var student4 = new Student() { Port = 5002 };
            var studentArray = new Student[] { student, student1, student2, student3, student4 };

            StudentDb.Insert(student);//插入

            StudentDb.InsertRange(studentArray);//批量插入

            var id = StudentDb.InsertReturnIdentity(student);//插入返回自增列

            //4.9.7.5我们可以转成 Insertable实现复杂插入
            StudentDb.AsInsertable(student);
            StudentDb.AsInsertable(student).ExecuteCommand();
            StudentDb.AsInsertable(studentArray).ExecuteCommand();
        }


        //更新例子
        public void UpdateDemo()
        {
            var student = new Student() { Id = 1, Port = 5000 };
            var studentArray = new Student[] { student };

            var result1 = StudentDb.Update(student);//根据实体更新
            var result2 = StudentDb.UpdateRange(studentArray);//批量更新
            var result3 = StudentDb.Update(it => new Student() { Port = 5000, CreateTime = DateTime.Now }, it => it.Id == 1);// 只更新Name列和CreateTime列，其它列不更新，条件id=1
            StudentDb.AsUpdateable(student);
            StudentDb.AsUpdateable(student).ExecuteCommand();
            //支持StudentDb.AsUpdateable(student)
        }

        //删除例子
        public void DeleteDemo()
        {
            var student = new Student() { Id = 1, Port = 5000 };

            var result1 = StudentDb.Delete(student);//根据实体删除
            var result2 = StudentDb.DeleteById(1);//根据主键删除
            var result3 = StudentDb.DeleteById(new int[] { 1, 2 });//根据主键数组删除
            var result4 = StudentDb.Delete(it => it.Id == 1);//根据条件删除
            //StudentDb.AsDeleteable().ExecuteCommand(); // 删除所有
            //支持StudentDb.AsDeleteable()
        }

        //使用事务的例子
        public void TranDemo(Action action)
        {

            var result = Db.Ado.UseTran(() =>
            {
                action();
                //这里写你的逻辑
            });
            if (result.IsSuccess)
            {
                //成功
            }
            else
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }

    }
}