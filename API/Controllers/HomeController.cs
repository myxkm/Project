using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using LY.Bussiness.Interface;
using LY.Bussiness.Service;
using LY.EF.Model;
using LY.Framework.Encrypt;
using LY.Framework.LoggerHelper;
using LY.Web.Core.IOC;
using LY.Web.Core.Models;
using Newtonsoft.Json;
using Unity;


namespace API.Controllers
{


    [AllowAnonymous]
    public class HomeController : Controller
    {
        protected Logger logger = Logger.CreateLogger(typeof(HomeController));
        //[ValidateAntiForgeryToken]
        public ActionResult Index()
        {
           var  keyPair = RsaEncrypt.GetKeyPair();
            var key= keyPair.Key;
            var value= keyPair.Value;
          var Encrypt= RsaEncrypt.Encrypt("1", key);
          var Decrypt = RsaEncrypt.Decrypt(Encrypt, value);

               var s = "a";
            new RedisHelper().Set("abc", s, 10);

            logger.Error("---初始化失败----");
            HttpApplication app = base.HttpContext.ApplicationInstance;

            List<SysEvent> sysEventsList = new List<SysEvent>();
            int i = 1;
            foreach (EventInfo e in app.GetType().GetEvents())
            {
                sysEventsList.Add(new SysEvent()
                {
                    Id = i++,
                    Name = e.Name,
                    TypeName = e.GetType().Name
                });
            }
            //return View(sysEventsList);
            var container = DIFactory.GetContainer();
            //using (ICategoryService service = container.Resolve<ICategoryService>())
            //{
            //    Caregory category = service.Find<Caregory>(1);
            //    service.Commit();
            //}
            //try
            //{
            //}
            //catch (Exception)
            //{
            //    throw;
            //}



            return View();
        }


        public ActionResult Error(int id)
        {

            

#if false
            CodeFristModel codeFristModel = new CodeFristModel();
            codeFristModel.Database.Log += c => Logger.CreateLogger(typeof(HomeController)).Debug(c);

            var sql = "insert into OJT_Master(log,OJT_Master_Id) values(@log,@OJTMasterId)";

            using (TransactionScope transactionScope = new TransactionScope())
            {
                OJTMaster OJTMasterModel = codeFristModel.OJTMaster.Add(new OJTMaster { Log = sql + sql + sql + sql + sql + sql + sql + sql + sql + sql + sql + sql + sql + sql });
                codeFristModel.SaveChanges();
                codeFristModel.OJTDetails.Add(new OJTDetails() { OJTMasterId = OJTMasterModel.Id, Log = sql + sql + sql + sql + sql + sql + sql + sql + sql + sql + sql + sql + sql + sql });
                codeFristModel.SaveChanges();
                transactionScope.Complete();
            }
            //codeFristModel.Configuration.LazyLoadingEnabled = false; 
            DbContextTransaction tran = codeFristModel.Database.BeginTransaction();
            try
            {
                codeFristModel.Database.ExecuteSqlCommand(sql, new SqlParameter[] { new SqlParameter("@log", sql + sql + sql + sql + sql + sql + sql), new SqlParameter("@OJTMasterId", 1) });
                tran.Commit();
            }
            catch (Exception exx)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
            }
            finally
            {
                tran.Dispose();
            }

            using (TransactionScope transa = new TransactionScope())  //EF事务提交
            {


                transa.Complete();
            }

            var list = codeFristModel.OJTMaster.Where(t => t.Id > 0);
            OJTMaster oJT = new OJTMaster();
            oJT = JsonConvert.DeserializeObject<OJTMaster>(JsonConvert.SerializeObject(list.First()));
            codeFristModel.Entry(oJT).State = EntityState.Modified;
            codeFristModel.Entry(oJT).Property("Log").IsModified = true;
            codeFristModel.OJTMaster.Attach(oJT);

            codeFristModel.SaveChanges();

            using (CodeFristModel context = new CodeFristModel())
            {
                var list1 = from u in context.OJT_User_Score_Master
                            join c in context.OJT_User_Score_Details
                            on u.Id equals c.User_Score_Master_Id
                            into uList
                            from s in uList.DefaultIfEmpty()
                            where u.Id == 1
                            orderby u.Id ascending
                            select new
                            {
                            }

                            ;

            }
#endif

            return View(id);
        }
    }

}
