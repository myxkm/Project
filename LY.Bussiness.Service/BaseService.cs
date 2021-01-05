using LY.Bussiness.Interface;
using LY.Framework.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;

namespace LY.Bussiness.Service
{
    public abstract class BaseService : IBaseService
    {
        protected DbContext Context { get; private set; }
        public BaseService(DbContext context)
        {
            this.Context = context;
        }
        #region 新增
        public T Add<T>(T t) where T : class
        {
            T dbt = this.Context.Set<T>().Add(t);
            this.Commit();
            return dbt;
        }
        public IEnumerable<T> Add<T>(IEnumerable<T> tList) where T : class
        {
            IEnumerable<T> dbt = this.Context.Set<T>().AddRange(tList);
            this.Commit();
            return dbt;
        }
        #endregion
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Delete<T>(T t) where T : class
        {
            if (t == null) throw new Exception("t is null");
            this.Context.Set<T>().Attach(t);
            this.Context.Set<T>().Remove(t);
            this.Commit();
        }
        /// <summary>
        /// 删除一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        public void Delete<T>(int Id) where T : class
        {
            T t = this.Find<T>(Id);
            if (t == null) throw new Exception("t is null");
            this.Context.Set<T>().Remove(t);
            this.Commit();
        }
        /// <summary>
        /// 删除多个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tList"></param>
        public void Delete<T>(IEnumerable<T> tList) where T : class
        {
            foreach (var t in tList)
            {
                this.Context.Set<T>().Attach(t);
            }
            this.Context.Set<T>().RemoveRange(tList);
            this.Commit();
        }
        #endregion
        #region 更新
        /// <summary>
        /// 不是从数据库获取的数据， 直接更新,需要Attach和State
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Update<T>(T t) where T : class
        {
            if (t == null) throw new Exception("t is null");

            this.Context.Set<T>().Attach(t);//将数据附加到上下文，支持实体修改和新实体，重置为UnChanged
            this.Context.Entry<T>(t).State = EntityState.Modified;
            this.Commit();//保存 然后重置为UnChanged
        }
        /// <summary>
        /// 不是从数据库过去的数据， 直接更新,需要Attach和State
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tList"></param>
        public void Update<T>(IEnumerable<T> tList) where T : class
        {
            foreach (var t in tList)
            {
                this.Context.Set<T>().Attach(t);
                this.Context.Entry<T>(t).State = EntityState.Modified;
            }
            this.Commit();
        }
        #endregion
        #region 查看
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T Find<T>(int t) where T : class
        {
            return this.Context.Set<T>().Find(t);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> Set<T>() where T : class
        {
            return this.Context.Set<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        public IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
            return this.Context.Set<T>().Where(funcWhere);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="funcWhere"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="funcOrderBy"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public PageResult<T> QueryPage<T, S>(Expression<Func<T, bool>> funcWhere, int PageIndex, int PageSize, Expression<Func<T, S>> funcOrderBy, bool isAsc = true) where T : class
        {
            var list = this.Set<T>();
            if (funcWhere != null) list = list.Where(funcWhere);
            if (isAsc) list.OrderBy(funcOrderBy);
            else list.OrderByDescending(funcOrderBy);
            PageResult<T> result = new PageResult<T>
            {
                DataList = list.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList(),
                PageIndex = PageIndex,
                PageSize = PageSize,
                TotalCount = list.Count()
            };
            return result;
        }
        #endregion

        /// <summary>
        /// 查看
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IQueryable<T> ExcuteQuery<T>(string sql, SqlParameter[] parameters) where T : class
        {
            return this.Context.Database.SqlQuery<T>(sql, parameters).AsQueryable();
        }
        /// <summary>
        /// 增删改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        public void Excute<T>(string sql, SqlParameter[] parameters) where T : class
        {
            DbContextTransaction trans = null;
            try
            {
                trans = this.Context.Database.BeginTransaction();
                this.Context.Database.ExecuteSqlCommand(sql, parameters);
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            finally
            {
                trans.Dispose();
            }
        }

        /// <summary>
        /// 获取修改--插入 数据库时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public DateTime GetDBDateTime<T>() where T : class
        {
            return this.Set<T>().Select(t => SqlFunctions.GetDate()).FirstOrDefault().Value;
        }

        public virtual void Dispose()
        {
            if (this.Context != null)
            {
                this.Context.Dispose();
            }
        }
        public void Commit()
        {
            this.Context.SaveChanges();
        }
    }
}
