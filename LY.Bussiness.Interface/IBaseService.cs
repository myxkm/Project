using LY.Framework.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace LY.Bussiness.Interface
{
    public interface IBaseService : IDisposable
    {
        T Add<T>(T t) where T : class;
        IEnumerable<T> Add<T>(IEnumerable<T> tList) where T : class;
        void Delete<T>(T t) where T : class;
        void Delete<T>(int Id) where T : class;
        void Delete<T>(IEnumerable<T> tList) where T : class;
        void Update<T>(T t) where T : class;
        void Update<T>(IEnumerable<T> tList) where T : class;
        T Find<T>(int t) where T : class;
        IQueryable<T> Set<T>() where T : class;
        IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class;
        PageResult<T> QueryPage<T, S>(Expression<Func<T, bool>> funcWhere, int PageIndex, int PageSize, Expression<Func<T, S>> funcOrderBy, bool isAsc = true) where T : class;
        IQueryable<T> ExcuteQuery<T>(string sql, SqlParameter[] parameters) where T : class;
        DateTime GetDBDateTime<T>() where T : class;
        void Excute<T>(string sql, SqlParameter[] parameters) where T : class;
        void Commit();

    }
}
